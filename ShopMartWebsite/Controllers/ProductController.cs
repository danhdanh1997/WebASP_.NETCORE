using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMartWebsite.Entities;
using ShopMartWebsite.Interfaces;
using ShopMartWebsite.Models;

namespace ShopMartWebsite.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index(string searchTerm, int? categoryId, int? page)
        {
            int recordSize = 4;
            page = page ?? 1;
            var model = new ProductListViewModel();
            model.categoryId = categoryId;
            model.Products = _productRepository.SearchProducts(searchTerm, categoryId, page.Value, recordSize);
            model.Categories = _categoryRepository.GetAllCategory();
            //tong so cot
            int totalRecords = _productRepository.SearchProductsCount(searchTerm, categoryId);
            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }
        
        public IActionResult Viewss(int ID)
        {
            var model = _productRepository.GetProductById(ID);
            return PartialView("_View",model);
        }
        [HttpGet]
        public IActionResult Action(int? ID)
        {
            var model = new ProductViewModel();
            if (ID.HasValue)// ID != 0 -> Update
            {
                var product = _productRepository.GetProductById(ID.Value);
                model.categoryId = product.categoryId;
                model.id = product.id;
                model.name = product.name;
                model.price = product.price;
                model.amount = product.amount;
                model.size = product.size;
                model.color = product.color;
                model.description = product.description;
                model.pictureIDs = product.image;
            }
            // Create
            //Categories to select category for product
            model.Categories = _categoryRepository.GetAllCategory();
            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(ProductViewModel model)
        {
            var pic = HttpContext.Request.Form.Files;
            JsonResult json;
            var result = false;
            string type = "";
            if (model.id > 0)//we are trying to edit a record
            {
                var product = _productRepository.GetProductById(model.id);
                product.id = model.id;
                product.categoryId = model.categoryId;
                product.name = model.name;
                product.price = model.price;
                product.amount = model.amount;
                product.size = model.size;
                product.color = model.color;
                product.description = model.description;
                product.image = model.pictureIDs;
                product.status = true;


                 result = _productRepository.UpdateProduct(product);
                type = "edit";
            }
            else    //we are trying to create a record
            {
                var product = new Product();
                product.categoryId = model.categoryId;
                product.name = model.name;
                product.price = model.price;
                product.size = model.size;
                product.amount = model.amount;
                product.color = model.color;
                product.description = model.description;
                product.image = model.pictureIDs;
                product.status = true;

                result = _productRepository.SaveProduct(product);
                type = "create";
            }

            if (result)
            {
                json = new JsonResult(new { Success = true, Type=type });
            }
            else
            {
                json = new JsonResult(new { Success = false, Type=type });
                
            }
            return json;
        }
        public IActionResult Delete(int ID)
        {
            var model = new ProductViewModel();
            var product = _productRepository.GetProductById(ID);
            model.id = product.id;
            return PartialView("_Delete",model);
        }
        [HttpPost]
        public JsonResult Delete(ProductViewModel model)
        {
            JsonResult json;
            var result = false;
            var product = _productRepository.GetProductById(model.id);
            product.status = false;
            result =_productRepository.DeleteProduct(product);

            if (result)
            {
                json = new JsonResult(new { Success = true, Message = "Xóa sản phẩm thành công!!!" });
                
            }
            else
            {
                json = new JsonResult(new { Success = false, Message = "Sản phẩm này đã được xóa!!!" });
            }

            return json;
        }
    }
}