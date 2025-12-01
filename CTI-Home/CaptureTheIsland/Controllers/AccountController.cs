using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CaptureTheIsland.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;

namespace CaptureTheIsland.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
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

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new ProfileViewModel
            {
                Email = user.Email,
                UserName = user.UserName
            };

            return View(model);
        }

        /// <summary>
        /// Toggles the Admin role for the given user (by email/username).
        /// Call this from other classes that have a reference to AccountController.
        /// </summary>
        /// <param name="email">User's email (also used as username).</param>
        /// <param name="makeAdmin">
        /// true = ensure user is in Admin role;
        /// false = ensure user is NOT in Admin role.
        /// </param>
        [NonAction]
        public async Task ToggleAdminAsync(string email, bool makeAdmin)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new System.ArgumentException("Email is required.", nameof(email));

            // Ensure the Admin role exists
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                if (!roleResult.Succeeded)
                {
                    var errors = string.Join("; ", roleResult.Errors.Select(e => e.Description));
                    throw new System.InvalidOperationException($"Failed to ensure Admin role exists: {errors}");
                }
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new System.InvalidOperationException($"User '{email}' not found.");
            }

            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (makeAdmin && !isAdmin)
            {
                var addResult = await _userManager.AddToRoleAsync(user, "Admin");
                if (!addResult.Succeeded)
                {
                    var errors = string.Join("; ", addResult.Errors.Select(e => e.Description));
                    throw new System.InvalidOperationException($"Failed to add user '{email}' to Admin role: {errors}");
                }
            }
            else if (!makeAdmin && isAdmin)
            {
                var removeResult = await _userManager.RemoveFromRoleAsync(user, "Admin");
                if (!removeResult.Succeeded)
                {
                    var errors = string.Join("; ", removeResult.Errors.Select(e => e.Description));
                    throw new System.InvalidOperationException($"Failed to remove user '{email}' from Admin role: {errors}");
                }
            }
        }

        // POST: /Account/ToggleAdmin
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleAdmin(string email, bool makeAdmin)
        {
            _logger.LogInformation("ToggleAdmin called. email={Email}, makeAdmin={MakeAdmin}, Caller={User}", email, makeAdmin, User?.Identity?.Name);

            // Prevent an admin from revoking their own Admin role
            if (!makeAdmin &&
                string.Equals(User?.Identity?.Name, email, StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "You cannot revoke your own Admin role.";
                _logger.LogWarning("User {User} attempted to revoke their own Admin role", User?.Identity?.Name);
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            try
            {
                await ToggleAdminAsync(email, makeAdmin);
                TempData["StatusMessage"] = makeAdmin
                    ? $"User '{email}' is now an Admin."
                    : $"User '{email}' is no longer an Admin.";

                _logger.LogInformation("ToggleAdmin succeeded for {Email}, makeAdmin={MakeAdmin}", email, makeAdmin);
            }
            catch (System.Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                _logger.LogError(ex, "ToggleAdmin failed for {Email}, makeAdmin={MakeAdmin}", email, makeAdmin);
            }

            // Redirect wherever your admin UI lives
            return RedirectToAction("Index", "Dashboard", new { area="Admin" });
        }


        /// <summary>
        /// Deletes a user by email/username.
        /// </summary>
        /// <param name="email">User's email (also used as username).</param>
        [NonAction]
        public async Task DeleteUserAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new InvalidOperationException($"User '{email}' not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to delete user '{email}': {errors}");
            }
        }

        // POST: /Account/DeleteUser
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string email)
        {
            _logger.LogInformation("DeleteUser called. email={Email}, Caller={User}",
                email,
                User?.Identity?.Name);

            if (string.IsNullOrWhiteSpace(email))
            {
                TempData["ErrorMessage"] = "Email is required.";
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            // Optional: prevent an admin from deleting their own account
            if (string.Equals(User?.Identity?.Name, email, StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "You cannot delete your own account.";
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            try
            {
                await DeleteUserAsync(email);

                TempData["StatusMessage"] = $"User '{email}' has been deleted.";
                _logger.LogInformation("DeleteUser succeeded for {Email}", email);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                _logger.LogError(ex, "DeleteUser failed for {Email}", email);
            }

            // Redirect back to your admin UI
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

    }
}
