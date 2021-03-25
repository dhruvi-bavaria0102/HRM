
using HRM.Data.Repository;
using HRM.BAL.Manager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Logging;

namespace HRM
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
            services.AddResponseCaching();
            services.AddMvc();
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<AppUser, IdentityRole>(config =>
            {
                // User defined password policy settings.  
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "HumanResourceManagerCookie";
                config.LoginPath = "/Home/Login"; // User defined login path  
                config.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });
            services.AddTransient(typeof(IEmployeeRepository), typeof(EmployeeRepository));
            services.AddTransient(typeof(IEmployeeManager), typeof(EmployeeManager));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile($"Logs/hrm-{DateTime.Today.Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseTimeMiddleware();
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Header-Name", "Dhruvi Bavaria");
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");
            });
        }
    }
}
