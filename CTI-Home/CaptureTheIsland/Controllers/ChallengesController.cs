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

        [HttpGet]
        public IActionResult HashStretch()
        {
            return View();
        }

        // 🟩 Hash Stretch — POST (Flag validator)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitHashStretch(string flag)
        {
            // SECRET correct answer — DO NOT display this publicly
            string correctFlag = "123456"; // example MD5 crack result

            if (!string.IsNullOrWhiteSpace(flag) &&
                flag.Trim().Equals(correctFlag, StringComparison.OrdinalIgnoreCase))
            {
                TempData["Message"] = "✔ Correct! Weak passwords make MD5 easy to break.";
                return RedirectToAction("HashStretch");
            }

            TempData["Error"] = "❌ Incorrect result. Try again — use the Resources table for help!";
            return RedirectToAction("HashStretch");
        }

        public IActionResult PNGOddities()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitPNGOddities(string flag)
        {
            string correctFlag = "CTI{you_found_the_flag}";

            if (flag != null && flag.Trim().Equals(correctFlag, StringComparison.OrdinalIgnoreCase))
            {
                TempData["Message"] = "✔ Correct! You uncovered the hidden metadata flag.";
                return RedirectToAction("PNGOddities");
            }

            TempData["Error"] = "❌ Incorrect flag. Keep investigating!";
            return RedirectToAction("PNGOddities");
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



