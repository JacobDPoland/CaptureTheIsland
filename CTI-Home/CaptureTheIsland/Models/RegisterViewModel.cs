using System.ComponentModel.DataAnnotations;

namespace CaptureTheIsland.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        // Require at least 10 characters to align with Identity options.
        [MinLength(10)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}

