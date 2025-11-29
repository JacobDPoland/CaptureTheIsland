using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CaptureTheIsland.Models;

namespace CaptureTheIsland.Data
{
    /// <summary>
    /// Seeds initial roles and a default administrator account into the
    /// application's database.  This mirrors the behaviour of the
    /// SecureExample project by creating the "Admin" and "User" roles
    /// if they do not already exist and seeding a single admin user
    /// with a strong password.  The seeded admin credentials are
    /// intended for demonstration purposes and should be changed in
    /// production deployments.
    /// </summary>
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            // Ensure the database is created and migrations are applied
            var context = scopedServices.GetRequiredService<ResourceContext>();
            await context.Database.MigrateAsync();

            var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();

            // Define the roles used by the application
            string[] roles = new[] { "Admin", "User" };

            // Create roles if they do not exist
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create a default administrator user (for demonstration)
            var adminEmail = "admin@example.com";
            // Ensure the password meets the policy defined in Startup.cs
            var adminPassword = "Admin123!@";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to create default admin user: {errors}");
                }
            }
        }
    }
}