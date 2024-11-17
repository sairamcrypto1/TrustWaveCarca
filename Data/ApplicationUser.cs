using Microsoft.AspNetCore.Identity;

namespace TrustWaveCarca.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public DateTime? LastLoginTime { get; set; } // Custom property for tracking last login

    }

}
