using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopMartWebsite.Entities
{
    [Table("product")]
    public class Product
    {
        public Product()
        {
            Comments = new List<Comment>();
        }
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        [Required]
        public int amount { get; set; }
        public string image { get; set; }
        [DefaultValue("true")]
        public bool status { get; set; }
        public Size? size { get; set; }
        public Color? color { get; set; }
        //foreign key
        public int? categoryId { get; set; }
        public Category category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
