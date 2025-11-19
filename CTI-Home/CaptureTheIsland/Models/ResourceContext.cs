using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CaptureTheIsland.Models
{
    public class ResourceContext : IdentityDbContext<IdentityUser>
    {
        public ResourceContext(DbContextOptions<ResourceContext> options)
            : base(options)
        {
        }

        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // VERY IMPORTANT

            modelBuilder.Entity<Resource>().HasData(
                new Resource
                {
                    ResourceId = 1,
                    Name = "dCode",
                    Link = "https://www.dcode.fr",
                    Description = "dCode is the universal site for deciphering coded messages..."
                },
                new Resource
                {
                    ResourceId = 2,
                    Name = "Cyber Chef",
                    Link = "https://gchq.github.io/CyberChef/",
                    Description = "Cyber Chef is a helpful cryptography resource..."
                },
                new Resource
                {
                    ResourceId = 3,
                    Name = "Whois",
                    Link = "https://www.who.is",
                    Description = "Helpful resource to find information about domain owners."
                }
            );
        }
    }
}
