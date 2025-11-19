using Microsoft.EntityFrameworkCore;

namespace CaptureTheIsland.Models
{
    public class ResourceContext : DbContext
    {
        public ResourceContext(DbContextOptions<ResourceContext> options) : base(options)
        { }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>().HasData(
                new Resource
                {
                    ResourceId = 1,
                    Name = "dCode",
                    Link = "https://www.dcode.fr",
                    Description = "dCode is the universal site for deciphering coded messages, cheating at word games, solving puzzles, geocaches and treasure hunts, etc."
                },
                new Resource
                {
                    ResourceId = 2,
                    Name = "Cyber Chef",
                    Link = "https://cyberchef.org",
                    Description = "Cyber Chef is a helpful cryptography resource. This site lets you chain together encoding methods and other tools."
                },
                new Resource
                {
                    ResourceId = 3,
                    Name = "Whois Domain Lookup",
                    Link = "https://www.whois.com/whois",
                    Description = "Helpful resource to find information about who owns a domain."
                }
            );
        }
    }
}
