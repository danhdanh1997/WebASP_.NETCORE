using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopMartWebsite.Entities;
using ShopMartWebsite.Extensions;
using ShopMartWebsite.Models;

namespace ShopMartWebsite.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        public AdminLoginController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AdminLoginController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authen(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return new OkObjectResult(new GenericResult(true));
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return new OkObjectResult(new GenericResult(false, " Tài khoản đã bị khóa"));
                }
                else
                {

                    return new OkObjectResult(new GenericResult(false, " Đăng nhập sai"));
                }
            }

            // If we got this far, something failed, redisplay form
            return new OkObjectResult(new GenericResult(false, model));
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return new OkObjectResult(new GenericResult(true));
        }
    }
}