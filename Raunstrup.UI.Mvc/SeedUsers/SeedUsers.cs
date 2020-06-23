using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raunstrup.UI.MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.SeedUsers
{
    public static class SeedUsers
    {
        // Vi henter Email og Password fra appsettings.json og sætte users oplysninger til tilsvarnde rolle i databasen.
        public static async Task InitializeAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var adminUserEmail = configuration["AdminUserEmail"];
            var adminUserPw = configuration["AdminUserPw"];
            await using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var adminId = await EnsureUserAsync(serviceProvider, adminUserEmail, adminUserPw).ConfigureAwait(false);
            await EnsureRoleAsync(serviceProvider, adminId, "Admin").ConfigureAwait(false);


            var userEmail = configuration["UserEmail"];
            var userPw = configuration["UserPw"];
            var userId = await EnsureUserAsync(serviceProvider, userEmail, userPw).ConfigureAwait(false);
            await EnsureRoleAsync(serviceProvider, userId, "User").ConfigureAwait(false);


            var managerEmail = configuration["ManagerEmail"];
            var managerPw = configuration["ManagerPw"];
            var managerId = await EnsureUserAsync(serviceProvider, managerEmail, managerPw).ConfigureAwait(false);
            await EnsureRoleAsync(serviceProvider, managerId, "Manager").ConfigureAwait(false);

 
            var user1 = await EnsureUserAsync(serviceProvider, "1", "ansat1").ConfigureAwait(false);
            await EnsureRoleAsync(serviceProvider, user1, "User").ConfigureAwait(false);

            var manager1 = await EnsureUserAsync(serviceProvider, "2", "ansat2").ConfigureAwait(false);
            await EnsureRoleAsync(serviceProvider, manager1, "Manager").ConfigureAwait(false);

            var admin1 = await EnsureUserAsync(serviceProvider, "3", "ansat3").ConfigureAwait(false);
            await EnsureRoleAsync(serviceProvider, admin1, "Admin").ConfigureAwait(false);
        }

        private static async Task<string> EnsureUserAsync(IServiceProvider serviceProvider,
            string userName, string userPw)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(userName).ConfigureAwait(false);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = userName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, userPw).ConfigureAwait(false);
            }

            if (user == null) throw new Exception("The password is probably not strong enough!");

            return user.Id;
        }

        private static async Task EnsureRoleAsync(IServiceProvider serviceProvider,
            string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null) throw new Exception("roleManager null");

            if (!await roleManager.RoleExistsAsync(role).ConfigureAwait(false))
                await roleManager.CreateAsync(new IdentityRole(role)).ConfigureAwait(false);

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid).ConfigureAwait(false);

            if (user == null) throw new Exception("The testUserPw password was probably not strong enough!");

            await userManager.AddToRoleAsync(user, role).ConfigureAwait(false);
        }
    }
}
