using Application.Common.Helper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Security.Claims;
using System.Threading.Tasks;
using Application.Common.Enums;

namespace WebUI.Helpers
{
    public static class MyIdentityDataInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("ntkyari@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "ntkyari@gmail.com";
                user.Email = "ntkyari@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "!SxcTQf.5H_8m8?").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Organisation Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Organisation Administrator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;

                roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(ClaimTypes.Permissions.GetAttributeStringValue(), "OrganisationUser.List")).Wait();
                roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(ClaimTypes.Permissions.GetAttributeStringValue(), "OrganisationUser.Create")).Wait();
                roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(ClaimTypes.Permissions.GetAttributeStringValue(), "OrganisationUser.Edit")).Wait();
                roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(ClaimTypes.Permissions.GetAttributeStringValue(), "OrganisationUser.View")).Wait();
            }

            if (!roleManager.RoleExistsAsync("Organisation User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Organisation User";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;

            }

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;

                roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(ClaimTypes.Permissions.GetAttributeStringValue(), "MenuCollection.List")).Wait();
                roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(ClaimTypes.Permissions.GetAttributeStringValue(), "MenuCollection.Create")).Wait();
                roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(ClaimTypes.Permissions.GetAttributeStringValue(), "MenuCollection.Update")).Wait();
            }
        }
    }
}
