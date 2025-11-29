using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using CaptureTheIsland.Data;

namespace CaptureTheIsland
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var startup = new Startup(builder.Configuration);
            // Configure services (Identity, EF Core, MVC, etc.)
            startup.ConfigureServices(builder.Services);

            // Build the application
            var app = builder.Build();

            // Seed roles and the default admin user before handling requests.
            // The SeedData class ensures that the database is migrated and
            // that the "Admin" and "User" roles exist along with a
            // demonstration administrator account.  Without seeding the
            // application would not have any roles defined and new
            // registrations would not be able to assign the "User" role.
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                // Perform the seeding synchronously by waiting on the
                // asynchronous task.  This is safe during startup and
                // avoids converting Main to async void/Task.
                CaptureTheIsland.Data.SeedData.InitializeAsync(services).GetAwaiter().GetResult();
            }

            // Configure the middleware pipeline and endpoint routing
            startup.Configure(app, app.Environment);
            app.Run();
        }
    }
}
