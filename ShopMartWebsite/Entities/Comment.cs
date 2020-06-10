using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopMartWebsite.Entities
{
    [Table("comment")]
    public class Comment
    {
        public Comment()
        {
            Replies = new List<Reply>();
        }
        public int id { get; set; }
        public string content { get; set; }
        public bool status { get; set; }
        public DateTime createDate { get; set; }
        //foreign key
        public int productId { get; set; }
        public Product product { get; set; }
        //foreign key
        public string userId { get; set; }
        public User user { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
