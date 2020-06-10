using ShopMartWebsite.Data;
using ShopMartWebsite.Entities;
using ShopMartWebsite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopMartWebsite.Services
{
    public class ProductRepository : IProductRepository
    {
        private ShopDbContext _ctx;
        public ProductRepository(ShopDbContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Product> GetProductsByCategoryId(int ID, int productId)
        {
            var products = _ctx.products.AsQueryable();
            return products.Where(x => x.categoryId == ID && x.id != productId && x.status == true && x.amount > 0).Include(Y=>Y.category).ToList();
        }
        public IEnumerable<Product> SearchProductsForHome(string searchTerm, int? categoryId, int page, int recordSize)
        {


            var products = _ctx.products.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(x => x.status == true && x.name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                products = products.Where(x => x.status == true && x.categoryId == categoryId.Value);
            }

            var skip = (page - 1) * recordSize;
            // skip  = (1    - 1) * 3 = 0 * 3 = 0
            // skip  = (2    - 1) * 3 = 1 * 3 = 3
            // skip  = (3    - 1) * 3 = 2 * 3 = 6

            return products.Where(x => x.status == true && x.amount > 0).Include(a => a.category).Include(cmt => cmt.Comments).OrderBy(x => x.categoryId).Skip(skip).Take(recordSize).ToList();
        }
        public IEnumerable<Product> SearchProducts(string searchTerm, int? categoryId, int page, int recordSize)
        {
            

            var products = _ctx.products.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(x => x.status==true && x.name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                products = products.Where(x => x.status == true && x.categoryId == categoryId.Value);
            }

            var skip = (page - 1) * recordSize;
            // skip  = (1    - 1) * 3 = 0 * 3 = 0
            // skip  = (2    - 1) * 3 = 1 * 3 = 3
            // skip  = (3    - 1) * 3 = 2 * 3 = 6

            return products.Where(x => x.status == true).Include(a=>a.category).Include(cmt=>cmt.Comments).OrderBy(x => x.categoryId).Skip(skip).Take(recordSize).ToList();
        }
        public bool DeleteProduct(Product product)
        {
            _ctx.Entry(product).State = EntityState.Modified;
            return _ctx.SaveChanges() > 0;
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _ctx.products.Where(x => x.status == true).Include(pro => pro.category).Include(cmt => cmt.Comments).ToList();
        }

        public Product GetProductById(int id)
        {
            return _ctx.products.Include(pro => pro.category).Include(cmt=>cmt.Comments).FirstOrDefault(x => x.id == id);
        }

        public bool SaveProduct(Product product)
        {
            if (product.name == null)
                return false;
            _ctx.products.Add(product);
            return _ctx.SaveChanges() > 0;
        }

        public bool UpdateProduct(Product product)
        {
            if (product.name == null)
                return false;
            _ctx.Entry(product).State = EntityState.Modified;
            return _ctx.SaveChanges() > 0;
        }
        public int SearchProductsCount(string searchTerm, int? categoryId)
        {
            

            var products = _ctx.products.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(x => x.status == true && x.name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                products = products.Where(x => x.status == true && x.categoryId == categoryId.Value);
            }



            return products.Where(x=>x.status==true).Count();
        }
        public int SearchProductsCountForHome(string searchTerm, int? categoryId)
        {


            var products = _ctx.products.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(x => x.status == true && x.name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                products = products.Where(x => x.status == true && x.categoryId == categoryId.Value);
            }



            return products.Where(x => x.status == true && x.amount > 0).Count();
        }
    }
}
