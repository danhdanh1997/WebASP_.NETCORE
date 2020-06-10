using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopMartWebsite.Entities;
using ShopMartWebsite.Interfaces;
using ShopMartWebsite.Models;

namespace ShopMartWebsite.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        public UserController(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string searchTerm, string roleID, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;
            //var abc = _signInManager.AuthenticationManager;
            UserListingViewModel model = new UserListingViewModel();
            model.SearchTerm = searchTerm;

            model.RoleID = roleID;
            //model.Roles = accomodationPackagesService.GetAllAccomodationPackages();

            model.Users = _userRepository.SearchUsers(searchTerm, page.Value, recordSize);

            

            var totalRecords = _userRepository.SearchUsersCount(searchTerm);
            //thuộc tính phân trang
            model.Pager = new Pager(totalRecords, page, recordSize);
            
            return View(model);
        }
        public async Task<IActionResult> Lockout(string ID, string Lockout)
        {
            var model = new UserViewModel();
            var user = await _userManager.FindByIdAsync(ID);
            if (Lockout == "true")
            {
                model.id = user.Id;
                model.Lockout = "true";
            }
            else
            {
                model.id = user.Id;
                model.Lockout = "false";
            }
            return PartialView("_Lockout", model);
        }
        [HttpPost]
        public async Task<IActionResult> Lockout(UserViewModel model)
        {

            JsonResult json;
            var user = await _userManager.FindByIdAsync(model.id);
            IdentityResult result = null;
            if (model.Lockout == "true")
            {
                user.LockoutEnd = DateTime.Now;
                result = await _userManager.UpdateAsync(user);
                json = new JsonResult(new { Success = true });
            }
            else
            {
                user.LockoutEnd = null;
                result = await _userManager.UpdateAsync(user);
                json = new JsonResult(new { Success = false });
            }



            return json;



        }
    }
}