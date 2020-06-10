using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Models
{
    public class ImageProductViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Alt { get; set; }
        //foreign key
        public int productId { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
