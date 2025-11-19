using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaptureTheIsland.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.ResourceId);
                });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "ResourceId", "Description", "Link", "Name" },
                values: new object[,]
                {
                    { 1, "dCode is the universal site for deciphering coded messages, cheating at word games, solving puzzles, geocaches and treasure hunts, etc.", "https://www.dcode.fr", "dCode" },
                    { 2, "Cyber Chef is a helpful cryptography resource. This site lets you chain together encoding methods and other tools.", "https://cyberchef.org", "Cyber Chef" },
                    { 3, "Helpful resource to find information about who owns a domain.", "https://www.whois.com/whois", "Whois Domain Lookup" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resource");
        }
    }
}
