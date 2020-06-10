using System;
using System.Collections.Generic;
using System.Linq;
using MimeKit;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopMartWebsite.Data;
using ShopMartWebsite.Entities;
using ShopMartWebsite.Models;
using ShopMartWebsite.Extensions;
using ShopMartWebsite.Interfaces;

namespace ShopMartWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ShopDbContext _ctx;
        private IUserRepository _userRepository;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, 
            ShopDbContext ctx, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _ctx = ctx;
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult Login(string messages, int? productId)
        {
            ViewData["notify"] = messages;
            var model = new RegisterViewModel();
            if (productId.HasValue)
            {
                model.productId = productId.Value;
            }
            else
                model.productId = null;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(RegisterViewModel model)
        {
            if (model.UserName == null || model.PasswordLogin == null || model.UserName.Length < 6 || model.PasswordLogin.Length < 6)
            {
                var data = new RegisterViewModel();

                return View("Login", data);
            }


            else if (model != null)
            {
                var user = _userRepository.GetUserByUserName(model.UserName);
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true

                if (user != null)
                {
                    if (user.EmailConfirmed == true)
                    {
                        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.PasswordLogin, false, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            if (model.productId.HasValue)
                            {
                                return RedirectToAction("Detail", "Home", new { productId = model.productId });
                            }
                            else
                                return RedirectToAction("Index", "Home");

                        }

                        else if (result.IsLockedOut)
                        {

                            //return new OkObjectResult(new GenericResult(false, " Tài khoản đã bị khóa"));
                            var data = new RegisterViewModel();
                            ViewData["LockedOutPassword"] = "Tài khoản đã bị khóa";
                            return View("Login", data);
                        }
                        else
                        {

                            //return new OkObjectResult(new GenericResult(false, " Đăng nhập sai"));
                            var data = new RegisterViewModel();
                            ViewData["WrongPassword"] = "Mật khẩu sai";
                            return View("Login", data);
                        }
                    }
                    else
                    {
                        var data = new RegisterViewModel();
                        ViewData["NotConfirmEmail"] = "Tài khoản chưa xác nhận Email!";
                        return View("Login", data);
                    }
                }
                else
                {
                    //return RedirectToAction("Login", "Account");
                    var data = new RegisterViewModel();
                    ViewData["NotAccount"] = "Tài khoản này chưa tạo!";
                    return View("Login", data);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        public IActionResult ConfirmEmail(string userid, string token)
        {
            var user = _userManager.FindByIdAsync(userid).Result;
            var result = _userManager.
                        ConfirmEmailAsync(user, token).Result;
            if (result.Succeeded)
            {
                string messages = "Email confirmed successfully!";
                return RedirectToAction("Login", "Account", messages);
            }
            else
            {
                string messages = "Error while confirming your email!";
                return RedirectToAction("Login", "Account", messages);
            }
        }
        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //var user = new User { UserName = model.Email, displayname = model.DisplayName, Email = model.Email };
            //var result = await _userManager.CreateAsync(user, model.Password);

            if (model.DisplayName == null || model.Email == null || model.Password == null || model.ConfirmPassword == null || model.Password != model.ConfirmPassword || model.Password.Length < 6 || model.ConfirmPassword.Length < 6)
            {
                var data = new RegisterViewModel();

                return View("Login", data);
            }

            else if (!_ctx.Users.Any(u => u.UserName == model.Email))
            {
                var user = new User { UserName = model.Email, displayname = model.DisplayName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var userRole = new Role("User");
                    if (!_ctx.Roles.Any(x => x.Name == "User"))
                    {
                        await _roleManager.CreateAsync(userRole);
                    }
                    await _userManager.AddToRoleAsync(user, userRole.Name);
                    //await _signInManager.SignInAsync(user, isPersistent: false);

                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callBackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = code }, protocol: HttpContext.Request.Scheme);
                    //await _userManager.SetEmailAsync()
                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Admin", "racingboywin1998@gmail.com"));
                    message.To.Add(new MailboxAddress("Confirm Register", user.Email));
                    message.Subject = "Xin vui lòng chạy link bên dưới để xác thực tài khoản!!!";
                    message.Body = new TextPart()
                    {
                        Text = callBackUrl
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("racingboywin1998@gmail.com", "thiendeptrai");
                        client.Send(message);
                        client.Disconnect(true);
                    };
                    //ViewData["notify"] = "Vui lòng vào Email để xác nhận tài khoản!!!";
                    //return View("Notify");
                    var data = new RegisterViewModel();
                    ViewData["RegisterSuccess"] = "Vui lòng vào Email để xác nhận tài khoản!!!";
                    return View("Login", data);

                }
            }
            else
            {

                var data = new RegisterViewModel();
                ViewData["notify"] = "Tài khoản đã đăng ký";
                return View("Login", data);
            }
            return Ok();
        }
        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordViewModel();
            return PartialView("_ForgotPassword", model);
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            JsonResult json;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token = token }, Request.Scheme);
                    //send mail for user
                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Admin", "racingboywin1998@gmail.com"));
                    message.To.Add(new MailboxAddress("Change Password", user.Email));
                    message.Subject = "Xin vui lòng chạy link bên dưới để đổi mật khẩu!!!";
                    message.Body = new TextPart()
                    {
                        Text = passwordResetLink
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("racingboywin1998@gmail.com", "thiendeptrai");
                        client.Send(message);
                        client.Disconnect(true);
                    };

                    //return View("ForgotPasswordConfirmation");
                    json = new JsonResult(new { Success = true });
                    return json;
                }
                json = new JsonResult(new { Success = false });
                return json;
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordViewModel();
            //model.Email = email;
            //model.Token = token;
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        //return View("ResetPasswordConfirmation");
                        //return new JsonResult(new { message = "Đặt lại mật khẩu thành công" });
                        ViewData["message"] = "Đặt lại mật khẩu thành công";
                        return View();
                    }
                    return View(model);
                }
            }
            return View(model);
        }
    }
}