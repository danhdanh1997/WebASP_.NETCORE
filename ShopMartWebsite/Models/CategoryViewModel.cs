
using ShopMartWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Models
{
    public class CategoryListViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool status { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }

}
