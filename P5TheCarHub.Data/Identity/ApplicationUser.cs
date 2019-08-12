using Microsoft.AspNetCore.Identity;

namespace P5TheCarHub.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(string userName) : base(userName)
        { }

        // Add additional proterties here.
    }
}