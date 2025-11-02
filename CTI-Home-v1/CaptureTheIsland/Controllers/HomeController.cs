using Microsoft.AspNetCore.Mvc;
using CaptureTheIsland.Models;
using System.Collections.Generic;

namespace CaptureTheIsland.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            IList<ChallengeSummary> challenges = ChallengeSummary.SampleData();
            return View(challenges);
        }
    }
}
