using ShopMartWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Models
{
    public class OrderDetailListViewModel
    {
        public string customer { get; set; }
        public decimal total { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
    public class OrderDetailViewModel
    {
    }
}
