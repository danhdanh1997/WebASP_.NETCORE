﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Entities
{
    [Table("AppRoles")]
    public class Role: IdentityRole
    {
        public Role() : base() { }
        public Role(string roleName) : base(roleName) { }
        public Role(string roleName, string description, DateTime creationDate):base(roleName)
        {
            this.Description = description;
            this.CreationDate = creationDate;
        }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
