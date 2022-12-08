using Domin.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.Seeds
{
    public static class DefaultRole
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            //if (!roleManager.Roles.Any())
            //{
                await roleManager.CreateAsync(new IdentityRole(Helper.Roles.SuperAdmin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Helper.Roles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Helper.Roles.Basic.ToString()));
            //}
        }
    }
}
