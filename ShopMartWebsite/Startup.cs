using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using ShopMartWebsite.Services;
using ShopMartWebsite.Data;
using ShopMartWebsite.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using ShopMartWebsite.Extensions;
using ShopMartWebsite.Helpers;
using Microsoft.AspNetCore.Http;
using ShopMartWebsite.Interfaces;

namespace ShopMartWebsite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("FirstConnection")));

            services.AddIdentity<User, Role>(options=> {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ShopDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddAuthentication().AddCookie();
            //services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsPrincipalFactory>();
            //services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsPrincipalFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IReplyRepository, ReplyRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    Configuration["EmailSender:Host"],
                    Configuration.GetValue<int>("EmailSender:Port"),
                    Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    Configuration["EmailSender:UserName"],
                    Configuration["EmailSender:Password"]
                )
            );


            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ILoggerFactory loggerFactory
            )
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            
            loggerFactory.AddFile("Logs/shop-{Date}.txt");
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapAreaRoute(
                    name: "areas",
                    areaName: "Admin",
                    template: "Admin/{controller}/{action}/{id?}",
                    defaults: new { controller = "Login", action = "Index" });
                /*routes.MapRoute(
                    name: "Admin",
                    template: "{controller=AdminLogin}/{action=Index}/{id?}"
                    );*/
            });
            
        }
    }
}
