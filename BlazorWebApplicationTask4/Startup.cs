using BlazorWebApplicationTask4.Areas.Identity.Data;
using BlazorWebApplicationTask4.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Radzen.Blazor;
using Radzen;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace BlazorWebApplicationTask4 {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("DefaultConnectioni");
        }

        public IConfiguration Configuration { get; }
        private readonly string _connectionString;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddAuthentication("Identity.Application")
                .AddCookie()
                .AddGoogle(options => {
                    options.ClientId = Configuration["Authentication:Google:Id"];
                    options.ClientSecret = Configuration["Authentication:Google:Secret"];
                })
                .AddFacebook(options => {
                    options.AppId = "1068783020596944";
                    options.AppSecret = "9bcd8f5b6212e8441d0648af3a377770";
                })
                .AddMicrosoftAccount(options => {
                    options.ClientId = "cf4cd86c-476c-4a6a-9453-55dddc095132";
                    options.ClientSecret = "8BZ7Q~nTKa.j.hZ0Gud4~0LBJdzMsPQOkSJu3";
                });
            //services.AddDbContextFactory<ApplicationDBContext>(options => 
            //    options.UseSqlServer(_connectionString));
            //services.AddDbContext<ApplicationDBContext>(options =>
            //    options.UseSqlServer(_connectionString));
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
            services.AddSingleton<WeatherForecastService>();
            services.AddLogging();
            services.AddScoped<NotificationService>();

            // ******
            // BLAZOR COOKIE Auth Code (begin)
            // From: https://github.com/aspnet/Blazor/issues/1554
            // HttpContextAccessor
            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            services.AddHttpClient();
            services.AddScoped<HttpClient>();
            // BLAZOR COOKIE Auth Code (end)
            // ******
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            //app.Use(async (context, next) => {
            //    if (context.Request.Path.Equals("/Logout", System.StringComparison.OrdinalIgnoreCase)) {
            //        await context.SignOutAsync();
            //    }
            //});

            app.UseEndpoints(endpoints => {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");

                // ******
                // BLAZOR COOKIE Auth Code (begin)
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                // BLAZOR COOKIE Auth Code (end)
                // ******
            });
        }
    }
}
