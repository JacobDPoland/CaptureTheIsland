using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CaptureTheIsland.Models
{
    // Use ApplicationUser instead of the base IdentityUser so that
    // additional profile properties can be added in the future and to
    // align with the SecureExample design.  ApplicationUser derives
    // from IdentityUser and resides in the same namespace.
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<ChallengeSummary> ChallengeSummaries { get; set; }

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
