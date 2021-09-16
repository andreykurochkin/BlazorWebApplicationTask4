using System;
using BlazorWebApplicationTask4.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BlazorWebApplicationTask4.Areas.Identity.IdentityHostingStartup))]
namespace BlazorWebApplicationTask4.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddDefaultIdentity<ApplicationUser>().
                    AddEntityFrameworkStores<ApplicationDBContext>();

                //services.AddIdentity<ApplicationUser, IdentityRole>()
                //    .AddEntityFrameworkStores<ApplicationDBContext>();
            });

            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<EmployeeManagementWebContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("EmployeeManagementWebContextConnection")));

            //    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<EmployeeManagementWebContext>();
            //});
        }
    }
}