using Microsoft.AspNetCore.Identity;

namespace CaptureTheIsland.Models
{
    /// <summary>
    /// Application-specific user that inherits from IdentityUser.  This
    /// class exists as a hook for adding future profile properties and to
    /// allow us to configure Identity using a concrete type instead of
    /// the base IdentityUser.  Without this class the Identity system
    /// cannot be extended to include additional fields (for example
    /// first/last names) in the future.  Even though it is currently
    /// empty it intentionally mirrors the design in the SecureExample
    /// project.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        // Additional profile data can be added here.  For now this
        // derived class simply marks our user type as distinct from
        // the framework's IdentityUser.
    }
}