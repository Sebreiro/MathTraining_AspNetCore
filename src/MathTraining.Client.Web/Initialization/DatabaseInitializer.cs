using System;
using System.Linq;
using System.Threading.Tasks;
using MathTraining.Data.Core;
using MathTraining.Data.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MathTraining.Client.Web.Initialization
{
    public class DatabaseInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MainContext>();

                await ApplyMigrations(context);

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                await SeedAdmin(userManager, roleManager);
            }
        }

        private static async Task ApplyMigrations(MainContext context)
        {
            await context.Database.MigrateAsync();
        }

        private static async Task SeedAdmin(
            UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager)
        {
            var roleName = "AdminRole";
            var userName = "Admin";
            var userPassword = "Password123";

            ApplicationRole adminRole;

            if (!roleManager.Roles.Any())
            {
                adminRole = new ApplicationRole()
                {
                    Name = roleName
                };
                await roleManager.CreateAsync(adminRole);
            }
            else
            {
                adminRole = roleManager.Roles.First(x => x.Name == roleName);
            }

            if (!userManager.Users.Any())
            {
                var adminUser = new ApplicationUser()
                {
                    UserName = userName,
                    LockoutEnabled = false,
                };
                var result = await userManager.CreateAsync(adminUser, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, roleName);
                }
            }
        }
    }
}
