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
        // ===============================
        // PASSWORD CRACKING MAIN LANDING
        // ===============================
        public IActionResult PasswordCracking()
        {
            return View();
        }

        // ===============================
        // EASY — ROCKYOU HASH CRACKING
        // ===============================
        [HttpGet]
        public IActionResult RockYou()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitRockYou(string hash1, string hash2, string hash3)
        {
            // Correct plaintext answers
            string a1 = "emilybffl";
            string a2 = "ryjd1982";
            string a3 = "kirkles";

            bool correct1 = !string.IsNullOrWhiteSpace(hash1) && hash1.Trim().ToLower() == a1;
            bool correct2 = !string.IsNullOrWhiteSpace(hash2) && hash2.Trim().ToLower() == a2;
            bool correct3 = !string.IsNullOrWhiteSpace(hash3) && hash3.Trim().ToLower() == a3;

            if (correct1 && correct2 && correct3)
            {
                TempData["Success"] = "✔ All three hashes cracked successfully!";
                TempData["Answer1"] = a1;
                TempData["Answer2"] = a2;
                TempData["Answer3"] = a3;
            }
            else
            {
                TempData["Error"] = "❌ One or more answers are incorrect. Try again!";
            }

            return RedirectToAction("RockYou");
        }

        // ===============================
        // MEDIUM — WINDOWS NTLM OPHCRACK
        // ===============================
        [HttpGet]
        public IActionResult WindowsHashes()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitWindowsHashes(string nt1, string nt2, string nt3)
        {
            string a1 = "footba11";
            string a2 = "starf0x";
            string a3 = "1trustno1";

            bool correct1 = nt1?.Trim().ToLower() == a1;
            bool correct2 = nt2?.Trim().ToLower() == a2;
            bool correct3 = nt3?.Trim().ToLower() == a3;

            if (correct1 && correct2 && correct3)
            {
                TempData["Success"] = "✔ Correct! All NTLM hashes cracked.";
                TempData["A1"] = a1;
                TempData["A2"] = a2;
                TempData["A3"] = a3;
            }
            else
            {
                TempData["Error"] = "❌ One or more answers incorrect.";
            }

            return RedirectToAction("WindowsHashes");
        }

        // ===============================
        // HARD — KALI LINUX YESCRYPT SHADOW
        // ===============================
        [HttpGet]
        public IActionResult KaliShadow()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitKaliShadow(
            string username,
            string date,
            string salt,
            string digest,
            string plaintext)
        {
            bool c1 = username?.Trim().ToLower() == "hollie";
            bool c2 = date?.Trim() == "2021-11-03";
            bool c3 = salt?.Trim() == "/WzixhAsn8sdXhCquYzh01$KZlio78LilItobsx";
            bool c4 = digest?.Trim() == "KZlio78LilItobsx/17ecFf1e2SbsduhP1sZEWuHrL4";
            bool c5 = plaintext?.Trim() == "hollie03";

            if (c1 && c2 && c3 && c4 && c5)
            {
                TempData["Success"] = "✔ All Kali Linux questions answered correctly!";
                TempData["U"] = "hollie";
                TempData["D"] = "2021-11-03";
                TempData["S"] = "/WzixhAsn8sdXhCquYzh01$KZlio78LilItobsx";
                TempData["H"] = "KZlio78LilItobsx/17ecFf1e2SbsduhP1sZEWuHrL4";
                TempData["P"] = "hollie03";
            }
            else
            {
                TempData["Error"] = "❌ One or more answers incorrect.";
            }

            return RedirectToAction("KaliShadow");
        }


        public IActionResult LogAnalysis()
        {
            return View();
        }

        public IActionResult LogEasy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitLogEasy(string q1, string q2, string q3, string q4)
        {
            bool correct =
                (q1?.Trim() == "172.16.0.7") &&
                (q2?.Trim() == "401") &&
                (q3?.Trim().ToLower() == "/images/banner.png") &&
                (q4?.Trim().ToUpper() == "POST");

            if (correct)
            {
                TempData["Success"] = "✔ All log analysis questions answered correctly!";
            }
            else
            {
                TempData["Error"] = "❌ Some answers were incorrect. Check the logs again.";
            }

            return RedirectToAction("LogEasy");
        }

        public IActionResult LogMedium()
        {
            return View();
        }

        public IActionResult LogHard()
        {
            return View();
        }


        public IActionResult Cryptography()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CryptoEasy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitCryptoEasy(string hexAnswer, string b64Answer, string binAnswer, string bigBinAnswer)
        {
            string correctHex = "scorpion";
            string correctB64 = "scribble";
            string correctBin = "securely";
            string correctBigBin = "lollipop";

            if (hexAnswer?.ToLower() == correctHex &&
                b64Answer?.ToLower() == correctB64 &&
                binAnswer?.ToLower() == correctBin &&
                bigBinAnswer?.ToLower() == correctBigBin)
            {
                TempData["Success"] = "✔ All answers are correct! Great job.";
            }
            else
            {
                TempData["Error"] = "❌ One or more answers are incorrect. Check your conversions.";
            }

            return RedirectToAction("CryptoEasy");
        }

        [HttpGet]
        public IActionResult CryptoMedium()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitCryptoMedium(string answer)
        {
            string correct = "I am not your instructor, I will not give you SKY-HIGH-8026";

            if (answer?.Trim().ToLower() == correct.ToLower())
            {
                TempData["Success"] = "✔ Correct! You decrypted the Vigenère cipher.";
                TempData["ShowSolution"] = true;
            }
            else
            {
                TempData["Error"] = "❌ Incorrect. Check your key and try again.";
            }

            return RedirectToAction("CryptoMedium");
        }

        public IActionResult CryptoHard()
        {
            return View();
        }


        public IActionResult Forensics()
        {
            return View();
        }

        public IActionResult ForensicsEasy()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult SubmitForensicsEasy(string q1, string q2, string q3, string q4)
        {
            // Correct answers
            string a1 = "gpeterson@mpd.hacknet.cityinthe.cloud";
            string a2 = "CTI-LRHX-4910";
            string a3 = "facebook";
            string a4 = "waffles85";

            if (q1?.Trim().ToLower() == a1.ToLower() &&
                q2?.Trim().ToUpper() == a2 &&
                q3?.Trim().ToLower() == a3 &&
                q4?.Trim() == a4)
            {
                TempData["Success"] = "✔ All answers correct! Great job.";
            }
            else
            {
                TempData["Error"] = "❌ One or more answers were incorrect. Try again!";
            }

            return RedirectToAction("ForensicsEasy");
        }

        public IActionResult ForensicsMedium()
        {
            return View();
        }

        public IActionResult ForensicsHard()
        {
            return View();
        }


        public IActionResult OSINT()
        {
            return View();
        }

        // =====================
        // OSINT EASY — Metadata
        // =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitOSINTEasy(string q1, string q2, string q3, string q4, string q5, string q6)
        {
            bool correct =
                q1?.Trim() == "2015-05-15 02:14" &&
                q2?.Trim().ToLower() == "1024x768" &&
                q3?.Trim().ToLower() == "apple" &&
                q4?.Trim().ToLower() == "iphone 5" &&
                q5?.Trim().ToLower() == "1/640" &&
                q6?.Trim().Replace(" ", "") == "39.8750,20.0100".Replace(" ", "");

            if (correct)
                TempData["Success"] = "✔ All answers correct! Great job analyzing metadata.";
            else
                TempData["Error"] = "❌ One or more answers are incorrect. Check the metadata again.";

            return RedirectToAction("OSINT_Easy");
        }


        // =====================
        // OSINT MEDIUM — Threat Intel
        // =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitOSINTMedium(string q1, string q2, string q3, string q4, string q5, string q6)
        {
            bool correct =
                q1?.Trim().ToUpper() == "CVE-2014-3566" &&
                q2?.Trim().ToLower() == "vsftpd 2.3.4" &&
                q3?.Trim().ToLower() == "1.0.1g" &&
                q4?.Trim().ToLower() == "854" ||
                q4?.Trim().ToLower() == "rfc 854" &&
                q5?.Trim() == "376" ||
                q5?.Trim().ToLower() == "376 bytes" &&
                q6?.Trim().ToLower() == "hero";

            if (correct)
                TempData["Success"] = "✔ All threat analysis answers correct!";
            else
                TempData["Error"] = "❌ Some answers are wrong. Verify using multiple sources.";

            return RedirectToAction("OSINT_Medium");
        }


        // =====================
        // OSINT HARD — Barcode
        // =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitOSINTHard(string format, string flag)
        {
            bool correct =
                format?.Trim().ToUpper() == "CODE-39" &&
                flag?.Trim().ToUpper() == "CTI-UZLU-5369";

            if (correct)
                TempData["Success"] = "✔ Correct! You decoded the barcode.";
            else
                TempData["Error"] = "❌ Incorrect. Try another barcode reader.";

            return RedirectToAction("OSINT_Hard");
        }

        public IActionResult Networking()
        {
            return View();
        }

        public IActionResult NetworkingEasy()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult SubmitNetworkingEasy(string q1, string q2, string q3, string q4, string q5)
        {
            string a1 = "AXFR";
            string a2 = "etas.com";
            string a3 = "4";
            string a4 = "3600";
            string a5 = "1.1.1.1";

            if (q1?.Trim().ToUpper() == a1 &&
                q2?.Trim().ToLower() == a2 &&
                q3?.Trim() == a3 &&
                q4?.Trim() == a4 &&
                q5?.Trim() == a5)
            {
                TempData["Success"] = "✔ All answers correct!";
            }
            else
            {
                TempData["Error"] = "❌ One or more answers were incorrect.";
            }

            return RedirectToAction("NetworkingEasy");
        }

        [HttpGet]
        public IActionResult NetworkingMedium()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitNetworkingMedium(string q1, string q2, string q3, string q4, string q5)
        {
            bool correct =
                q1?.Trim().ToLower() == "wget" &&
                q2?.Trim().ToLower() == "nginx" &&
                q3?.Trim() == "174.143.213.184" &&
                q4?.Trim() == "192.168.1.140" &&
                q5?.Trim().ToUpper() == "966007C476E0C200FBA8B28B250A6379";

            if (correct)
            {
                TempData["Success"] = "✔ All answers correct! Great job analyzing HTTP traffic.";
            }
            else
            {
                TempData["Error"] = "❌ One or more answers are incorrect. Re-check the PCAP in Wireshark.";
            }

            return RedirectToAction("NetworkingMedium");
        }


        public IActionResult NetworkingHard()
        {
            return View();
        }


        public IActionResult Scripting()
        {
            return View();
        }

        public IActionResult PythonEasy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitPythonEasy(string answer)
        {
            string correct = "mSeeeeeeee"; // your solution

            if (answer != null && answer.Trim() == correct)
            {
                TempData["Success"] = "✔ Correct! Nice job analyzing the Python script.";
            }
            else
            {
                TempData["Error"] = "❌ Incorrect. Try reverse-engineering the script again!";
            }

            return RedirectToAction("PythonEasy");
        }

        // GET — Medium Python Challenge
        public IActionResult PythonMedium()
        {
            return View();
        }

        // POST — Validate Medium Python Answer
        [HttpPost]
        public IActionResult SubmitPythonMedium(string answer)
        {
            string correct = "mysupersecretpassword";

            if (answer?.Trim() == correct)
            {
                TempData["Success"] = "✔ Correct! Great job reversing the compiled Python logic.";
            }
            else
            {
                TempData["Error"] = "❌ Incorrect. Try reversing the shift cipher in vals.";
            }

            return RedirectToAction("PythonMedium");
        }

        public IActionResult PythonHard()
        {
            return View(); // Coming soon page if you want
        }
    }
}



