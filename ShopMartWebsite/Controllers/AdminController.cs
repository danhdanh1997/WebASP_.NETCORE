using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopMartWebsite.Entities;
using ShopMartWebsite.Extensions;
using ShopMartWebsite.Interfaces;

namespace ShopMartWebsite.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AdminController(ICategoryRepository categoryRepository, UserManager<User> userManager, IHostingEnvironment he)
        {
            _categoryRepository = categoryRepository;
            _userManager = userManager;
            _hostingEnvironment = he;
        }
        
        public IActionResult Index()
        {
            //var model = new User();
            //System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            //model.displayname = currentUser.Identity.Name;
            //model.displayname = user.displayname;
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "AdminLogin");

            return View();
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Produces("application/json")]
        public async  Task<IActionResult> UploadPictures()
        {

            var files = HttpContext.Request.Form.Files;
            List<string> picList = new List<string>();
            for (var i = 0; i < files.Count; i++)
            {
                var picture = files[i];

                var filename = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                var pathToImagesFolder = Path.Combine(_hostingEnvironment.WebRootPath + "/images/site/", Path.GetFileName(filename));
                picList.Add(filename);
                await picture.CopyToAsync(new FileStream(pathToImagesFolder, FileMode.Create));
            }

            return Json(picList);
        }   
    }
}