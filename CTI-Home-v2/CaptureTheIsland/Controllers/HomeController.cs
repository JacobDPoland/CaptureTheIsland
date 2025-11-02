using Microsoft.AspNetCore.Mvc;
using CaptureTheIsland.Models;
using System.Collections.Generic;

namespace CaptureTheIsland.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var vm = new LandingViewModel
            {
                Modules = ModuleInfo.OfferedModules(),
                Challenges = ChallengeSummary.SampleData()
            };
            return View(vm);
        }
    }
}
