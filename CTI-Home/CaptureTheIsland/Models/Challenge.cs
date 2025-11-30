using Microsoft.AspNetCore.Mvc;

namespace CaptureTheIsland.Models
{
    public class Challenge : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
