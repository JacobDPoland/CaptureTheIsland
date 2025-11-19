using CaptureTheIsland.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CaptureTheIsland
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            // --- ENABLE MVC ---
            services.AddControllersWithViews();

            // --- DATABASE CONTEXT ---
            services.AddDbContext<ResourceContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ResourceContext")));

            // --- ASP.NET CORE IDENTITY ---
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ResourceContext>()
                .AddDefaultTokenProviders();

            // --- COOKIE SETTINGS (REDIRECT TO LOGIN) ---
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";          // Redirect here when not logged in
                options.AccessDeniedPath = "/Account/Login";   // Redirect here if unauthorized
            });

            // --- ROUTING CONFIG ---
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // --- AUTHENTICATION + AUTHORIZATION MIDDLEWARE ---
            app.UseAuthentication();
            app.UseAuthorization();

            // --- ENDPOINT ROUTING ---
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");
            });
        }
    }
}
