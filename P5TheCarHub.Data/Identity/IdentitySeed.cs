using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Infrastructure.Identity
{
    public static class IdentitySeed
    {
        private const string AdminUser = "Admin_";
        private const string AdminPassword = "P@ssword123!";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = (UserManager<ApplicationUser>)scope.ServiceProvider.GetService(typeof(UserManager<ApplicationUser>));
               
                ApplicationUser user = await userManager.FindByIdAsync(AdminUser);

                if (user == null)
                {
                   await userManager.CreateAsync(new ApplicationUser("Admin"), AdminPassword);
                }
            }
        }
    }
}
