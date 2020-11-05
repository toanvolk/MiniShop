using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiniShop.EF;
using MiniShop.Identity;
using MiniShop.Identity.Data;
using System.Threading.Tasks;

namespace MiniShop.Web
{
    public static class MigrateDatabaseAuto
    {

        public static void Migrate(IApplicationBuilder app, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            using (var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<MiniShopContext>())
                {
                    context.Database.Migrate();
                }
                using (var context = serviceScope.ServiceProvider.GetService<AuthDbContext>())
                {
                    context.Database.Migrate();

                    #region Role and User default
                    //role
                    IdentityResult roleResult;
                    bool adminRoleExists = roleManager.RoleExistsAsync("Admin").Result;
                    if (!adminRoleExists)
                    {
                        roleResult = roleManager.CreateAsync(new IdentityRole("Admin")).Result;
                    }

                    //user
                    var authUser = new IdentityUser()
                    {
                        UserName = "Admin",
                        Email = "toanvolk@gmail.com",
                        EmailConfirmed = true
                    };

                    if (userManager.FindByEmailAsync(authUser.Email).Result == null)
                    {
                        var result = userManager.CreateAsync(authUser, "To@nvo92").Result;

                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(authUser, "Admin").Wait();
                        }
                    }
                    #endregion

                }
            }
        }
    }
}
