using ShopMartWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Models
{
    public class OrderListViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        //public IEnumerable<Category> Categories { get; set; }

        public DateTime? searchDate  { get; set; }

        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }
    public class OrderViewModel
    {
        public int id { get; set; }
        public bool status { get; set; }
        public bool confirm { get; set; }
        public decimal total { get; set; }
        public string address { get; set; }
        //đối vs guest
        public string customer { get; set; }
        public string info { get; set; }
        public string note { get; set; }
        public DateTime createDate { get; set; }
        //foreign key
        public string userId { get; set; }
        public User user { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
