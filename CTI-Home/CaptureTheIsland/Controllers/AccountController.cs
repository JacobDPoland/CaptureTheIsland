using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CaptureTheIsland.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace CaptureTheIsland.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ============================
        // GET: /Account/Login
        // ============================
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // ============================
        // POST: /Account/Login
        // ============================
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                model.ErrorMessage = string.Join(" | ", errors);
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                model.ErrorMessage = "Invalid login attempt.";
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                model.Password,
                false,
                false
            );

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            model.ErrorMessage = "Invalid login attempt.";
            return View(model);
        }

        // ============================
        // GET: /Account/Register
        // ============================
        [HttpGet]
        public IActionResult Register() => View();

        // ============================
        // POST: /Account/Register
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Create a new application user with the supplied email.  We use
            // ApplicationUser instead of IdentityUser so that Identity will
            // persist our user type and allow us to extend the user model in
            // the future.  EmailConfirmed is left false here; users can be
            // required to confirm their email before activation by adding
            // additional logic.
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Newly registered users are automatically placed in the
                // "User" role.  The roles are created during application
                // startup by the SeedData class.  Assigning the role
                // ensures that authorization attributes function correctly.
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            // If creation failed, surface the errors to the user
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // ============================
        // POST: /Account/Logout
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        // ============================
        // GET: /Account/AccessDenied
        // ============================
        /// <summary>
        /// Displays a friendly access denied page when an authenticated user attempts
        /// to access a resource they are not authorized to view.  Marked AllowAnonymous
        /// so that the page can be shown even if the user does not belong to the
        /// required role for the requested resource.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
