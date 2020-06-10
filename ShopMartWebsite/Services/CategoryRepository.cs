using Microsoft.EntityFrameworkCore;
using ShopMartWebsite.Data;
using ShopMartWebsite.Entities;
using ShopMartWebsite.Interfaces;
using ShopMartWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ShopMartWebsite.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private ShopDbContext _ctx;

        public CategoryRepository(ShopDbContext ctx)
        {
            _ctx = ctx;
        }
        public int SearchCategoriesCount(string searchTerm)
        {


            var categories = _ctx.categories.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                categories = categories.Where(x => x.name.ToLower().Contains(searchTerm.ToLower()));
            }
            return categories.Where(x=> x.status==true).Count();
        }
        public bool DeleteCategory(Category category)
        {
            _ctx.Entry(category).State = EntityState.Modified;
            return _ctx.SaveChanges() > 0;
        }

        public IEnumerable<Category> GetAllCategory()
        {
            return _ctx.categories.Include(cate => cate.Products);
        }

        public Category GetCategoryById(int id)
        {
            return _ctx.categories.Include(pro => pro.Products).FirstOrDefault(x => x.id == id);
        }

        public bool SaveCategory(Category category)
        {
            if (category.name == null)
                return false;
            _ctx.categories.Add(category);

            return _ctx.SaveChanges() > 0;
        }

        public IEnumerable<Category> SearchCategories(string searchTerm, int page, int recordSize)
        {
            var categories = _ctx.categories.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                categories = categories.Where(x => x.name.ToLower().Contains(searchTerm.ToLower()));
            }
            

            var skip = (page - 1) * recordSize;
            // skip  = (1    - 1) * 3 = 0 * 3 = 0
            // skip  = (2    - 1) * 3 = 1 * 3 = 3
            // skip  = (3    - 1) * 3 = 2 * 3 = 6

            return categories.Where(x => x.status == true).Include(a => a.Products).OrderBy(x => x.id).Skip(skip).Take(recordSize).ToList();
           
        }

        public bool UpdateCategory(Category category)
        {
            if (category.name == null)
                return false;
            _ctx.Entry(category).State = EntityState.Modified;
            return _ctx.SaveChanges() > 0;
        }
    }
}
