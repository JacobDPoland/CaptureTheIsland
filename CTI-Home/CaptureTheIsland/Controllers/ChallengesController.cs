using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaptureTheIsland.Controllers
{
[Authorize(Roles = "User,Admin")] // Users MUST be logged in and in the correct role to see challenges
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

        public  IActionResult RockYou()
        {
            return View();
        }

        // ===============================
        // EASY — ROCKYOU HASH CRACKING
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitRockYou(string hash1, string hash2, string hash3)
        {
            // Correct plaintext answers
            string a1 = "emilybffl";
            string a2 = "ryjd1982";
            string a3 = "kirkles";

            bool anyCorrect = false;
            bool anyWrong = false;

            // Validate #1 only if user entered something
            if (!string.IsNullOrWhiteSpace(hash1))
            {
                if (hash1.Trim().ToLower() == a1)
                {
                    TempData["Correct1"] = true;
                    TempData["Answer1"] = a1;
                    anyCorrect = true;
                }
                else
                {
                    TempData["Correct1"] = false;
                    anyWrong = true;
                }
            }

            // Validate #2 only if user entered something
            if (!string.IsNullOrWhiteSpace(hash2))
            {
                if (hash2.Trim().ToLower() == a2)
                {
                    TempData["Correct2"] = true;
                    TempData["Answer2"] = a2;
                    anyCorrect = true;
                }
                else
                {
                    TempData["Correct2"] = false;
                    anyWrong = true;
                }
            }

            // Validate #3 only if user entered something
            if (!string.IsNullOrWhiteSpace(hash3))
            {
                if (hash3.Trim().ToLower() == a3)
                {
                    TempData["Correct3"] = true;
                    TempData["Answer3"] = a3;
                    anyCorrect = true;
                }
                else
                {
                    TempData["Correct3"] = false;
                    anyWrong = true;
                }
            }

            // Case 1: User submitted NOTHING
            if (string.IsNullOrWhiteSpace(hash1) &&
                string.IsNullOrWhiteSpace(hash2) &&
                string.IsNullOrWhiteSpace(hash3))
            {
                TempData["Error"] = "⚠ Please enter at least one answer.";
                return RedirectToAction("RockYou");
            }

            // Case 2: All attempted answers correct
            if (anyCorrect && !anyWrong)
            {
                TempData["Success"] = "✔ Great job! All submitted answers were correct!";
                return RedirectToAction("RockYou");
            }

            // Case 3: Some correct, some wrong
            if (anyCorrect && anyWrong)
            {
                TempData["Error"] = "⚠ Some answers were correct, but some were incorrect. Keep trying!";
                return RedirectToAction("RockYou");
            }

            // Case 4: All attempted answers wrong
            TempData["Error"] = "❌ All entered answers were incorrect. Try again!";
            return RedirectToAction("RockYou");
        }

        public IActionResult WindowsHashes()
        {
            return View();
        }

        // ===============================
        // MEDIUM — WINDOWS NTLM OPHCRACK
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitWindowsHashes(string nt1, string nt2, string nt3)
        {
            // Correct NTLM plaintext answers
            string a1 = "footba11";
            string a2 = "starf0x";
            string a3 = "1trustno1";

            bool anyCorrect = false;
            bool anyWrong = false;

            // Validate only if answer is provided
            if (!string.IsNullOrWhiteSpace(nt1))
            {
                if (nt1.Trim().ToLower() == a1)
                {
                    TempData["Correct1"] = true;
                    TempData["A1"] = a1;
                    anyCorrect = true;
                }
                else
                {
                    TempData["Correct1"] = false;
                    anyWrong = true;
                }
            }

            if (!string.IsNullOrWhiteSpace(nt2))
            {
                if (nt2.Trim().ToLower() == a2)
                {
                    TempData["Correct2"] = true;
                    TempData["A2"] = a2;
                    anyCorrect = true;
                }
                else
                {
                    TempData["Correct2"] = false;
                    anyWrong = true;
                }
            }

            if (!string.IsNullOrWhiteSpace(nt3))
            {
                if (nt3.Trim().ToLower() == a3)
                {
                    TempData["Correct3"] = true;
                    TempData["A3"] = a3;
                    anyCorrect = true;
                }
                else
                {
                    TempData["Correct3"] = false;
                    anyWrong = true;
                }
            }

            // ⭐ User entered nothing
            if (string.IsNullOrWhiteSpace(nt1) &&
                string.IsNullOrWhiteSpace(nt2) &&
                string.IsNullOrWhiteSpace(nt3))
            {
                TempData["Error"] = "⚠ Please enter at least one answer.";
                return RedirectToAction("WindowsHashes");
            }

            // ⭐ All attempted answers correct
            if (anyCorrect && !anyWrong)
            {
                TempData["Success"] = "✔ Great job! All submitted NTLM hashes were correct.";
                return RedirectToAction("WindowsHashes");
            }

            // ⭐ Some correct, some wrong
            if (anyCorrect && anyWrong)
            {
                TempData["Error"] = "⚠ Some answers were correct, but some were incorrect. Keep trying!";
                return RedirectToAction("WindowsHashes");
            }

            // ⭐ All attempted answers wrong
            TempData["Error"] = "❌ All entered answers were incorrect. Try again!";
            return RedirectToAction("WindowsHashes");
        }

        public IActionResult KaliShadow()
        {
            return View();
        }

        // ===============================
        // HARD — KALI LINUX YESCRYPT SHADOW
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitKaliShadow(
     string username,
     string date,
     string salt,
     string digest,
     string plaintext)
        {
            // Correct values
            string Auser = "hollie";
            string Adate = "2021-11-03";
            string Asalt = "/WzixhAsn8sdXhCquYzh01$KZlio78LilItobsx";
            string Adigest = "KZlio78LilItobsx/17ecFf1e2SbsduhP1sZEWuHrL4";
            string Aplain = "hollie03";

            bool anyCorrect = false;
            bool anyWrong = false;

            // USERNAME
            if (!string.IsNullOrWhiteSpace(username))
            {
                if (username.Trim().ToLower() == Auser)
                {
                    TempData["CorrectUser"] = true;
                    TempData["U"] = Auser;
                    anyCorrect = true;
                }
                else
                {
                    TempData["CorrectUser"] = false;
                    anyWrong = true;
                }
            }

            // DATE
            if (!string.IsNullOrWhiteSpace(date))
            {
                if (date.Trim() == Adate)
                {
                    TempData["CorrectDate"] = true;
                    TempData["D"] = Adate;
                    anyCorrect = true;
                }
                else
                {
                    TempData["CorrectDate"] = false;
                    anyWrong = true;
                }
            }

            // SALT
            if (!string.IsNullOrWhiteSpace(salt))
            {
                if (salt.Trim() == Asalt)
                {
                    TempData["CorrectSalt"] = true;
                    TempData["S"] = Asalt;
                    anyCorrect = true;
                }
                else
                {
                    TempData["CorrectSalt"] = false;
                    anyWrong = true;
                }
            }

            // DIGEST
            if (!string.IsNullOrWhiteSpace(digest))
            {
                if (digest.Trim() == Adigest)
                {
                    TempData["CorrectDigest"] = true;
                    TempData["H"] = Adigest;
                    anyCorrect = true;
                }
                else
                {
                    TempData["CorrectDigest"] = false;
                    anyWrong = true;
                }
            }

            // PLAINTEXT
            if (!string.IsNullOrWhiteSpace(plaintext))
            {
                if (plaintext.Trim() == Aplain)
                {
                    TempData["CorrectPlain"] = true;
                    TempData["P"] = Aplain;
                    anyCorrect = true;
                }
                else
                {
                    TempData["CorrectPlain"] = false;
                    anyWrong = true;
                }
            }

            // ⭐ User entered nothing
            if (string.IsNullOrWhiteSpace(username) &&
                string.IsNullOrWhiteSpace(date) &&
                string.IsNullOrWhiteSpace(salt) &&
                string.IsNullOrWhiteSpace(digest) &&
                string.IsNullOrWhiteSpace(plaintext))
            {
                TempData["Error"] = "⚠ Please enter at least one answer.";
                return RedirectToAction("KaliShadow");
            }

            // ⭐ All attempted answers were correct
            if (anyCorrect && !anyWrong)
            {
                TempData["Success"] = "✔ All submitted answers were correct!";
                return RedirectToAction("KaliShadow");
            }

            // ⭐ Some correct, some wrong
            if (anyCorrect && anyWrong)
            {
                TempData["Error"] = "⚠ Some answers were correct, but some were incorrect. Keep going!";
                return RedirectToAction("KaliShadow");
            }

            // ⭐ All submitted answers were wrong
            TempData["Error"] = "❌ All entered answers were incorrect. Try again!";
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
            // Correct answers
            string a1 = "172.16.0.7";
            string a2 = "401";
            string a3 = "/images/banner.png";
            string a4 = "POST";

            int totalCorrect = 0;

            // Check each answer individually
            if (!string.IsNullOrWhiteSpace(q1) && q1.Trim() == a1)
                totalCorrect++;

            if (!string.IsNullOrWhiteSpace(q2) && q2.Trim() == a2)
                totalCorrect++;

            if (!string.IsNullOrWhiteSpace(q3) && q3.Trim().ToLower() == a3)
                totalCorrect++;

            if (!string.IsNullOrWhiteSpace(q4) && q4.Trim().ToUpper() == a4)
                totalCorrect++;

            // If user got at least *one* correct
            if (totalCorrect > 0)
            {
                TempData["Success"] = $"✔ You answered {totalCorrect} question(s) correctly!";
                TempData["A1"] = a1;
                TempData["A2"] = a2;
                TempData["A3"] = a3;
                TempData["A4"] = a4;
            }
            else
            {
                TempData["Error"] = "❌ None of the submitted answers were correct — try again!";
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
        public IActionResult SubmitCryptoEasy(
     string hexAnswer,
     string b64Answer,
     string binAnswer,
     string bigBinAnswer)
        {
            // Correct answers
            string a1 = "scorpion";
            string a2 = "scribble";
            string a3 = "securely";
            string a4 = "lollipop";

            int correct = 0;

            // Individual checks
            if (!string.IsNullOrWhiteSpace(hexAnswer) &&
                hexAnswer.Trim().ToLower() == a1)
                correct++;

            if (!string.IsNullOrWhiteSpace(b64Answer) &&
                b64Answer.Trim().ToLower() == a2)
                correct++;

            if (!string.IsNullOrWhiteSpace(binAnswer) &&
                binAnswer.Trim().ToLower() == a3)
                correct++;

            if (!string.IsNullOrWhiteSpace(bigBinAnswer) &&
                bigBinAnswer.Trim().ToLower() == a4)
                correct++;

            // Response handling
            if (correct > 0)
            {
                TempData["Success"] = $"✔ You answered {correct} question(s) correctly!";
                TempData["A1"] = a1;
                TempData["A2"] = a2;
                TempData["A3"] = a3;
                TempData["A4"] = a4;
            }
            else
            {
                TempData["Error"] = "❌ None of your answers were correct. Try again!";
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

            if (!string.IsNullOrWhiteSpace(answer))
            {
                string user = answer.Trim().ToLower();
                string sol = correct.ToLower();

                // Exact match
                if (user == sol)
                {
                    TempData["Success"] = "✔ Correct! You decrypted the Vigenère cipher.";
                    TempData["ShowSolution"] = true;
                }
                else
                {
                    /* OPTIONAL CLOSE MATCH FEATURE
                    if (sol.Contains(user) || user.Contains(sol.Substring(0, 20)))
                    {
                        TempData["Error"] = "⚠ Your answer is close, but not exact. Check capitalization, punctuation, or spacing.";
                    }
                    else
                    */
                    {
                        TempData["Error"] = "❌ Incorrect. Check your key and try again.";
                    }
                }
            }
            else
            {
                TempData["Error"] = "❌ Please enter an answer before submitting.";
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

            bool correct1 = !string.IsNullOrWhiteSpace(q1) &&
                             q1.Trim().ToLower() == a1.ToLower();

            bool correct2 = !string.IsNullOrWhiteSpace(q2) &&
                             q2.Trim().ToUpper() == a2;

            bool correct3 = !string.IsNullOrWhiteSpace(q3) &&
                             q3.Trim().ToLower() == a3;

            bool correct4 = !string.IsNullOrWhiteSpace(q4) &&
                             q4.Trim() == a4;

            // If ANY are correct — partial credit allowed
            if (correct1 || correct2 || correct3 || correct4)
            {
                TempData["Success"] = "✔ Correct answers submitted!";
            }

            // If ANY fields are filled AND wrong
            if ((!string.IsNullOrWhiteSpace(q1) && !correct1) ||
                (!string.IsNullOrWhiteSpace(q2) && !correct2) ||
                (!string.IsNullOrWhiteSpace(q3) && !correct3) ||
                (!string.IsNullOrWhiteSpace(q4) && !correct4))
            {
                TempData["Error"] = "❌ One or more answers are incorrect. Try again!";
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
            // Correct Answers
            string a1 = "2015-05-15 02:14";
            string a2 = "1024x768";
            string a3 = "apple";
            string a4 = "iphone 5";
            string a5 = "1/640";
            string a6 = "39.8750,20.0100";

            bool correct1 = !string.IsNullOrWhiteSpace(q1) && q1.Trim() == a1;
            bool correct2 = !string.IsNullOrWhiteSpace(q2) && q2.Trim().ToLower() == a2.ToLower();
            bool correct3 = !string.IsNullOrWhiteSpace(q3) && q3.Trim().ToLower() == a3;
            bool correct4 = !string.IsNullOrWhiteSpace(q4) && q4.Trim().ToLower() == a4;
            bool correct5 = !string.IsNullOrWhiteSpace(q5) && q5.Trim().ToLower() == a5.ToLower();
            bool correct6 = !string.IsNullOrWhiteSpace(q6) &&
                             q6.Trim().Replace(" ", "").ToLower() ==
                             a6.Replace(" ", "").ToLower();

            // Show success if ANY answers are correct
            if (correct1 || correct2 || correct3 || correct4 || correct5 || correct6)
                TempData["Success"] = "✔ Correct answers submitted!";

            // Show error only for fields that were filled AND incorrect
            if ((!string.IsNullOrWhiteSpace(q1) && !correct1) ||
                (!string.IsNullOrWhiteSpace(q2) && !correct2) ||
                (!string.IsNullOrWhiteSpace(q3) && !correct3) ||
                (!string.IsNullOrWhiteSpace(q4) && !correct4) ||
                (!string.IsNullOrWhiteSpace(q5) && !correct5) ||
                (!string.IsNullOrWhiteSpace(q6) && !correct6))
            {
                TempData["Error"] = "❌ One or more answers are incorrect. Check the metadata again.";
            }

            return RedirectToAction("OSINT_Easy");
        }



        // =====================
        // OSINT MEDIUM — Threat Intel
        // =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitOSINTMedium(string q1, string q2, string q3, string q4, string q5, string q6)
        {
            // Correct answers
            string a1 = "CVE-2014-3566";
            string a2 = "vsftpd 2.3.4";
            string a3 = "1.0.1g";
            string a4 = "854";           // RFC number only
            string a5 = "376";           // bytes
            string a6 = "hero";

            // Normalize RFC answers (allow "RFC 854")
            string norm4 = q4?.Trim().ToLower().Replace("rfc", "").Trim();

            // Normalize byte answers (allow "376 bytes")
            string norm5 = q5?.Trim().ToLower().Replace("bytes", "").Trim();

            bool correct1 = !string.IsNullOrWhiteSpace(q1) && q1.Trim().ToUpper() == a1;
            bool correct2 = !string.IsNullOrWhiteSpace(q2) && q2.Trim().ToLower() == a2;
            bool correct3 = !string.IsNullOrWhiteSpace(q3) && q3.Trim().ToLower() == a3;
            bool correct4 = !string.IsNullOrWhiteSpace(q4) && norm4 == a4;
            bool correct5 = !string.IsNullOrWhiteSpace(q5) && norm5 == a5;
            bool correct6 = !string.IsNullOrWhiteSpace(q6) && q6.Trim().ToLower() == a6;

            // If ANY answers are correct -> success
            if (correct1 || correct2 || correct3 || correct4 || correct5 || correct6)
                TempData["Success"] = "✔ Correct answers submitted!";

            // If ANY filled answers are wrong -> error
            if ((!string.IsNullOrWhiteSpace(q1) && !correct1) ||
                (!string.IsNullOrWhiteSpace(q2) && !correct2) ||
                (!string.IsNullOrWhiteSpace(q3) && !correct3) ||
                (!string.IsNullOrWhiteSpace(q4) && !correct4) ||
                (!string.IsNullOrWhiteSpace(q5) && !correct5) ||
                (!string.IsNullOrWhiteSpace(q6) && !correct6))
            {
                TempData["Error"] = "❌ One or more answers were incorrect.";
            }

            return RedirectToAction("OSINT_Medium");
        }



        // =====================
        // OSINT HARD — Barcode
        // =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitOSINTHard(string format, string flag)
        {
            // Correct answers
            string a1 = "CODE-39";
            string a2 = "CTI-UZLU-5369";

            bool correct1 = !string.IsNullOrWhiteSpace(format) &&
                            format.Trim().ToUpper() == a1;

            bool correct2 = !string.IsNullOrWhiteSpace(flag) &&
                            flag.Trim().ToUpper() == a2;

            // If ANY answer is correct → success
            if (correct1 || correct2)
                TempData["Success"] = "✔ Correct answers submitted!";

            // If any *filled* answer is wrong → error
            if ((!string.IsNullOrWhiteSpace(format) && !correct1) ||
                (!string.IsNullOrWhiteSpace(flag) && !correct2))
            {
                TempData["Error"] = "❌ One or more answers are incorrect. Try again!";
            }

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
            // Correct answers
            string a1 = "AXFR";
            string a2 = "etas.com";
            string a3 = "4";
            string a4 = "3600";
            string a5 = "1.1.1.1";

            bool correct1 = !string.IsNullOrWhiteSpace(q1) &&
                            q1.Trim().ToUpper() == a1;

            bool correct2 = !string.IsNullOrWhiteSpace(q2) &&
                            q2.Trim().ToLower() == a2;

            bool correct3 = !string.IsNullOrWhiteSpace(q3) &&
                            q3.Trim() == a3;

            bool correct4 = !string.IsNullOrWhiteSpace(q4) &&
                            q4.Trim() == a4;

            bool correct5 = !string.IsNullOrWhiteSpace(q5) &&
                            q5.Trim() == a5;

            // ⭐ Success if ANY answer is correct
            if (correct1 || correct2 || correct3 || correct4 || correct5)
                TempData["Success"] = "✔ Correct answers submitted!";

            // ❗ Error only if a filled box is wrong
            if ((!string.IsNullOrWhiteSpace(q1) && !correct1) ||
                (!string.IsNullOrWhiteSpace(q2) && !correct2) ||
                (!string.IsNullOrWhiteSpace(q3) && !correct3) ||
                (!string.IsNullOrWhiteSpace(q4) && !correct4) ||
                (!string.IsNullOrWhiteSpace(q5) && !correct5))
            {
                TempData["Error"] = "❌ One or more answers are incorrect. Try again!";
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



