using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopMartWebsite.Entities;

namespace ShopMartWebsite.Data
{
    public class ShopDbContext : IdentityDbContext<User,Role,string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public static ShopDbContext Create()
        {
            return new ShopDbContext();
        }

        public ShopDbContext()
        {
        }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }

        public ShopDbContext(DbContextOptions<ShopDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Reply> replies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
    }
}
