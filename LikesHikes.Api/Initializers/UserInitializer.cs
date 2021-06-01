using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikesHikes.Api.Initializers
{
    public class UserInitializer
    {
        private const string AdminRoleName = nameof(UserRole.Admin);
        private const string UserRoleName = nameof(UserRole.User);

        private const string AdminUserName = "Admin";
        private const string AdminUserEmail = "admin@admin.com";
        private const string AdminUserPassword = "Admin1!";

        public static async Task InitializeAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            await CreateRole(roleManager, AdminRoleName);
            await CreateRole(roleManager, UserRoleName);

            await CreateUser(userManager, AdminUserEmail, AdminUserName, AdminUserPassword, AdminRoleName);
        }

        private static async Task CreateUser(UserManager<AppUser> userManager, string userEmail, string adminUserName, string userPassword, string roleName)
        {
            if (await userManager.FindByNameAsync(adminUserName) == null)
            {
                AppUser admin = new AppUser { Email = userEmail, UserName = adminUserName };
                var result = await userManager.CreateAsync(admin, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, roleName);
                }
            }
        }
        private static async Task CreateRole(RoleManager<AppRole> roleManager, string roleName)
        {
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new AppRole(roleName));
            }
        }
    }
}
