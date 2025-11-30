using Microsoft.AspNetCore.Mvc;
using CaptureTheIsland.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CaptureTheIsland.Controllers
{
    // Require a logged‑in user in either the User or Admin roles for all
    // actions in this controller.  This replaces the generic [Authorize]
    // attribute so that only those assigned to one of our defined roles
    // can access the home page and associated information.
    [Authorize(Roles = "User,Admin")]   // 🔒 THIS PROTECTS THE ENTIRE HOME CONTROLLER
    public class HomeController : Controller
    {
        private ApplicationContext context { get; set; }

        public HomeController(ApplicationContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var vm = new LandingViewModel
            {
                Modules = ModuleInfo.OfferedModules(),
                Challenges = ChallengeSummary.SampleData(),
                Resources = context.Resources
                    .OrderBy(r => r.Name)
                    .ToList()
            };
            return View(vm);
        }
        public IActionResult About()
        {
            return View();
        }

        // ⭐ TEAM PAGE
        public IActionResult Team()
        {
            return View();
        }

        // ⭐ CONTACT PAGE
        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Contact(string name, string email, string subject, string message)
        {
            TempData["Message"] = "Your message has been received! We'll get back to you soon.";
            return RedirectToAction("Contact");
        }

    }

}
