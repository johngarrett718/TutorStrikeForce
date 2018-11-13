using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TutorStrikeForce.EF;
using TutorStrikeForce.Models;

namespace TutorStrikeForce.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost SeedRoles(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                const string adminRole = "Admin";

                var roleExist = roleManager.RoleExistsAsync(adminRole).Result;
                if (!roleExist)
                {
                    roleManager.CreateAsync(new IdentityRole(adminRole)).Wait();
                }
            }

            return host;
        }

        public static IWebHost SeedUsers(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                if(userManager.FindByEmailAsync("admin@tsf.com").Result == null)
                {
                    var newUser = new ApplicationUser
                    {
                        UserName = "admin@tsf.com",
                        Email = "admin@tsf.com"
                    };

                    Task<IdentityResult> identityResult = userManager.CreateAsync(newUser, "password");
                    identityResult.Wait();

                    if (identityResult.Result.Succeeded)
                    {
                        userManager.AddToRoleAsync(newUser, "Admin");
                    }
                }
            }

            return host;
        }

        public static IWebHost SeedApplicationData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<TutorStrikeForceContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return host;
        }
    }
}
