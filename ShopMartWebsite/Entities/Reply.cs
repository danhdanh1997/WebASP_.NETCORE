using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopMartWebsite.Entities
{
    [Table("reply")]
    public class Reply
    {
        public int id { get; set; }
        public string content { get; set; }
        public bool status { get; set; }
        public DateTime createDate { get; set; }
        //foreign key
        public string userId { get; set; }
        public User user { get; set; }
        //foreign key
        public int commentId { get; set; }
        public Comment comment { get; set; }
    }
}
