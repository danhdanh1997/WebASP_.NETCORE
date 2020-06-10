using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopMartWebsite.Data;
using ShopMartWebsite.Entities;

namespace ShopMartWebsite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            // tạo Admin nếu chưa cài chưa có tài khoản admin(seed data)
            var host = BuildWebHost(args).Build();
            try
            {
                var scope = host.Services.CreateScope();

                var ctx = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
                var userMnr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleMnr = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                ctx.Database.EnsureCreated();
                var adminRole = new Role("Admin");
                if (!ctx.Roles.Any())
                {
                    roleMnr.CreateAsync(adminRole).GetAwaiter().GetResult();
                }
                if (!ctx.Users.Any(u => u.UserName == "admin"))
                {
                    var adminUser = new User
                    {
                        UserName = "admin",//username Admin , password = password.
                        Email = "admin@gmail.com"
                    };
                    var resule = userMnr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                    userMnr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                }
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
            }
            host.Run();
        }
        
        public static IWebHostBuilder BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        /*public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
            
               .UseStartup<Startup>();*/
    }
}
