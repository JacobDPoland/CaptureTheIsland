using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaptureTheIsland.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller for the admin dashboard area.  Only users in the "Admin"
    /// role may access this area.  The dashboard provides a landing page
    /// for administrative functions such as managing resources or
    /// overseeing user activity.  At present it simply displays a
    /// welcome message but can be extended to include additional
    /// administrative features.
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        /// <summary>
        /// Displays the admin dashboard.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}