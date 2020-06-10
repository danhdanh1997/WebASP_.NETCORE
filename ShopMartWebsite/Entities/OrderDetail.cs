using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopMartWebsite.Entities
{
    [Table("orderdetail")]
    public class OrderDetail
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public decimal unitPrice { get; set; }
        //foreign key
        public int productId { get; set; }
        public Product product { get; set; }
        //foreign key
        public int orderId { get; set; }
        public Order order { get; set; }
    }
}
