using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMartWebsite.Entities;
using ShopMartWebsite.Interfaces;
using ShopMartWebsite.Models;

namespace ShopMartWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index(string searchTerm, int? page)
        {
            int recordSize = 4;
            page = page ?? 1;
            var model = new CategoryListViewModel();
            model.SearchTerm = searchTerm;
            model.Categories = _categoryRepository.SearchCategories(searchTerm, page.Value, recordSize);
            
            //tong so cot
            int totalRecords = _categoryRepository.SearchCategoriesCount(searchTerm);
            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);
        }
        [HttpGet]
        public IActionResult Action(int? ID)
        {
            var model = new CategoryViewModel();
            if (ID.HasValue)// ID != 0 -> Update
            {
                var category = _categoryRepository.GetCategoryById(ID.Value);
                model.Id = category.id;
                model.Name = category.name;
            
                model.Products = category.Products;
            }
            // Create
            //Categories to select category for product
            
            return PartialView("_Action",model);
        }
        [HttpPost]
        public JsonResult Action(CategoryViewModel model)
        {
            JsonResult json;
            var result = false;
            string type = "";
            if (model.Id > 0)//we are trying to edit a record
            {
                var category = _categoryRepository.GetCategoryById(model.Id);

                category.name = model.Name;
                
                category.Products = model.Products;
                


                result = _categoryRepository.UpdateCategory(category);
                type = "edit";
                
            }
            else    //we are trying to create a record
            {
                var category = new Category();
                category.name = model.Name;
                category.status = true;


                
                result = _categoryRepository.SaveCategory(category);
                type = "create";
            }

            if (result)
            {
                json = new JsonResult(new { Success = true, Type = type });
                
            }
            else
            {
                json = new JsonResult(new { Success = false, Type = type });
                
            }

            return json;
        }
        [HttpGet]
        public IActionResult Delete(int ID)
        {
            var model = new CategoryViewModel();
            var category = _categoryRepository.GetCategoryById(ID);
            model.Id = category.id;
            return PartialView("_Delete", model);
        }
        [HttpPost]
        public JsonResult Delete(CategoryViewModel model)
        {
            JsonResult json;
            var result = false;
            var category = _categoryRepository.GetCategoryById(model.Id);
            //xóa logic
            category.status = false;
            result = _categoryRepository.DeleteCategory(category);

            if (result)
            {
                json = new JsonResult(new { Success = true });

            }
            else
            {
                json = new JsonResult(new { Success = false, Message = "Danh mục xóa không thành công!!!" });
            }

            return json;
        }
    }
}