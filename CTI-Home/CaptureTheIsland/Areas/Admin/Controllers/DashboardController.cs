using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CaptureTheIsland.Models;
using System.IO;

namespace CaptureTheIsland.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var allUsers = _userManager.Users.ToList();
            var allChallenges = GetAllChallenges();

            var model = new AdminDashboardViewModel
            {
                TotalUsers = allUsers.Count,
                AdminCount = allUsers.Count(u => _userManager.IsInRoleAsync(u, "Admin").Result),
                TotalChallenges = allChallenges.Count,
                TotalCompletedChallenges = 0, // can't track per-user completions on server yet

                Users = allUsers.Select(u => new AdminUserRow
                {
                    Email = u.Email,
                    Role = _userManager.IsInRoleAsync(u, "Admin").Result ? "Admin" : "User",
                    // placeholder "joined" date – replace with a real field later if you add one
                    DateCreated = DateTime.UtcNow,
                    CompletedChallenges = 0
                }).ToList()
            };

            return View(model);
        }

        private List<string> GetAllChallenges()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Challenges");

            return Directory.GetFiles(path, "*.cshtml")
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .Where(name => name != "Index") // ignore category index page
                .ToList();
        }
    }
}
