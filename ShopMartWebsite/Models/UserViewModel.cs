using Microsoft.AspNetCore.Identity;
using ShopMartWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Models
{
    public class UserListingViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public string RoleID { get; set; }
        public List<Role> Roles { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }
    public class UserViewModel
    {
        public string id { get; set; }
        public string displayname { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public bool status { get; set; }
        public bool gender { get; set; }
        public string tempGender { get; set; }
        public DateTime birthDay { get; set; }
        public string Lockout { get; set; }
    }

}
