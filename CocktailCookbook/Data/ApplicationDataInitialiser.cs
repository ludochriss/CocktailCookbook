using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Data
{
    public class ApplicationDataInitialiser
    {
        
            public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                SeedRoles(roleManager);
                SeedUsers(userManager);
            }

            public static void SeedUsers(UserManager<IdentityUser> userManager)
            {
                if (userManager.FindByNameAsync("admin@admin.com").Result == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = "admin@admin.com",
                        Email = "admin@admin.com",
                        NormalizedUserName = "ADMIN@ADMIN.COM",
                        NormalizedEmail = "ADMIN@ADMIN.COM",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        SecurityStamp = Guid.NewGuid().ToString()

                    };

                    string password = "Admin!1";

                    var result = userManager.CreateAsync(user, password).Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Administrator").Wait();
                    }
                }
            }

            public static void SeedRoles(RoleManager<IdentityRole> roleManager)
            {
                if (!roleManager.RoleExistsAsync("Administrator").Result)
                {
                    var role = new IdentityRole
                    {
                        Name = "Administrator"
                    };
                    roleManager.CreateAsync(role).Wait();
                }
            }
        }
    }

