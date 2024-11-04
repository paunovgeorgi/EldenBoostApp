using EldenBoost.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using static EldenBoost.Common.Constants.GeneralApplicationConstants;

namespace EldenBoost.Extenstions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task CreateAdminRoleAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (userManager != null && roleManager != null && await roleManager.RoleExistsAsync(AdminRole) == false)
            {
                var role = new IdentityRole(AdminRole);
                await roleManager.CreateAsync(role);

                var admin = await userManager.FindByEmailAsync("admin@admin.com");

                if (admin != null)
                {
                    await userManager.AddToRoleAsync(admin, role.Name!);
                }
            }
        }
    }
}
