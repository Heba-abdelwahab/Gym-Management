using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Seeding
{
    public static class IdentitySeeding
    {
        public static async Task RoleSeeding(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Trainee"));
                await roleManager.CreateAsync(new IdentityRole("Coach"));
                await roleManager.CreateAsync(new IdentityRole("Owner"));
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }
    }
}
