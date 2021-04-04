using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace TennisAssociation.Models
{
    public static class IdentitySeedData
    {
        public static async void AddDefaultRole(IApplicationBuilder app)
        {
            UserManager<MyUser> userManager = app.ApplicationServices.GetRequiredService<UserManager<MyUser>>();
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();

            var roleExists = await roleManager.RoleExistsAsync("Admin");
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            roleExists = await roleManager.RoleExistsAsync("Basic");
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("Basic"));
            }
        }
    }
}