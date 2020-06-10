using Microsoft.AspNetCore.Http;
using ShopMartWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Models
{
    public class ProductListViewModel
    {
        
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public int? categoryId{ get; set; }

        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }
    public class ProductViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public bool status { get; set; }
        public string image { get; set; }
        public Size? size { get; set; }
        public Color? color { get; set; }
        //foreign key
        public int? categoryId { get; set; }
        public Category category { get; set; }
        public string pictureIDs { get; set; }
        
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Product> ProductsOfCategory { get; set; }

    }
}
