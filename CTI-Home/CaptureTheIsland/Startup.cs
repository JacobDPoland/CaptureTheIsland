using CaptureTheIsland.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using CaptureTheIsland.Models;

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
            // Configure Identity to use our ApplicationUser class and require
            // strong passwords, unique emails and account lockout on repeated
            // failures.  These settings mirror the SecureExample project and
            // ensure that credentials are stored using the built‑in Identity
            // infrastructure.  The AddEntityFrameworkStores call wires up
            // Identity to use our ResourceContext (which derives from
            // IdentityDbContext<ApplicationUser>).  Token providers are added
            // to support password resets and other token based flows.
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Enforce a strong password policy
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 10;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                // User settings
                options.User.RequireUniqueEmail = true;

                // Lockout settings
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
                .AddEntityFrameworkStores<ResourceContext>()
                .AddDefaultTokenProviders();

            // --- COOKIE SETTINGS (REDIRECT TO LOGIN) ---
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";          // Redirect here when not logged in
                // Redirect unauthorized users to a dedicated access denied page instead of the
                // generic login page.  This improves usability by making it clear that the
                // user is authenticated but lacks permission to view the requested resource.
                options.AccessDeniedPath = "/Account/AccessDenied";
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
                // Area routing must come before the default route.  This
                // pattern matches controllers inside Areas and directs
                // requests to the appropriate dashboard or other area
                // controllers.  Without this route, area controllers would
                // not be discovered.
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                // Default route for non‑area controllers.  The slug
                // parameter is optional and used by some challenge views.
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");
            });
        }
    }
}
