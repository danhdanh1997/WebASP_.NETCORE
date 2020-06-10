using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopMartWebsite.Entities
{
    [Table("order")]
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
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
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
