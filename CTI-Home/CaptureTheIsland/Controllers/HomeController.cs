using Microsoft.AspNetCore.Mvc;
using CaptureTheIsland.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace CaptureTheIsland.Controllers
{
    public class HomeController : Controller
    {
        private ResourceContext context { get; set; }

        public HomeController(ResourceContext ctx)
        {
            context = ctx;
        }
        public  IActionResult Index()
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
    }
}
