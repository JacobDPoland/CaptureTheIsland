using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CaptureTheIsland.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [ValidateNever]   // ⬅️ ADD THIS LINE
        public string ErrorMessage { get; set; }
    }
}