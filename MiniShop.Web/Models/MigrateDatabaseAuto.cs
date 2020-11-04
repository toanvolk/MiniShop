using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiniShop.EF;
using MiniShop.Identity.Data;

namespace MiniShop.Web
{
    public static class MigrateDatabaseAuto
    {        
        public static void Migrate(IApplicationBuilder app)
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
                }
            }
        }
    }
}
