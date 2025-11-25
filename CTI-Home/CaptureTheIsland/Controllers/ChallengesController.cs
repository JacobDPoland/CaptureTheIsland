using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaptureTheIsland.Controllers
{
    [Authorize] // Users MUST be logged in to see challenges
    public class ChallengesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // 🧩 Sample challenge group
        public IActionResult Sample()
        {
            return View();
        }

        // 🟦 Apache Access Sleuth
        public IActionResult ApacheAccessSleuth()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitApacheAccessSleuth(string flag)
        {
            // Correct flag (based on your .txt image clues)
            string correctFlag = "evil_script.sh";

            if (flag != null && flag.Trim().ToLower() == correctFlag.ToLower())
            {
                TempData["Message"] = "✔ Correct! Nice job detecting the suspicious file.";
                return RedirectToAction("ApacheAccessSleuth");
            }

            TempData["Error"] = "❌ Incorrect flag. Try analyzing the logs again.";
            return RedirectToAction("ApacheAccessSleuth");
        }

        // 🟦 Cipher Warm-up — GET
        [HttpGet]
        public IActionResult CipherWarmup(bool? correct = null)
        {
            ViewBag.Correct = correct;
            return View();
        }

        // 🟩 Cipher Warm-up — POST (Flag Validation)
        [HttpPost]
        public IActionResult SubmitCipherWarmup(string flag)
        {
            string correctFlag = "Hello, this is my flag";

            if (flag?.Trim().Equals(correctFlag, StringComparison.OrdinalIgnoreCase) == true)
            {
                TempData["Success"] = "Correct! Well done.";
            }
            else
            {
                TempData["Error"] = "Incorrect flag. Try again!";
            }

            return RedirectToAction("CipherWarmup");
        }

        // 📚 Categories for navigation
        public IActionResult PasswordCracking()
        {
            return View();
        }

        public IActionResult LogAnalysis()
        {
            return View();
        }

        public IActionResult Cryptography()
        {
            return View();
        }

        public IActionResult Forensics()
        {
            return View();
        }

        public IActionResult OSINT()
        {
            return View();
        }

        public IActionResult Networking()
        {
            return View();
        }

        public IActionResult Scripting()
        {
            return View();
        }
    }
}



