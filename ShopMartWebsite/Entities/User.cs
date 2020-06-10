using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopMartWebsite.Entities
{

    public class User : IdentityUser
    {

        public string displayname { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public bool status { get; set; }
        public bool gender { get; set; }
        public DateTime birthDay { get; set; }

        public string password { get; set; }
    }
}
