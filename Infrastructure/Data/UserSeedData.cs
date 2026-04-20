using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public static class UserSeedData
    {
        private readonly static string adminPassword = "Admin@123";
        public static void UserSeed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();//DI because static dont have access to constructor so inject here
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Name = SystemRole.Admin.ToString(),Code=SystemRole.Admin, },
                    new Role { Name = SystemRole.Employee.ToString(),Code=SystemRole.Employee },
                    new Role { Name = SystemRole.User.ToString(),Code=SystemRole.User }
                };
                context.Roles.AddRange(roles);
                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                var adminRoleId = context.Roles.FirstOrDefault(r => r.Code == SystemRole.Admin).Id;
                var user = new User
                {
                    Name = "Admin User",
                    Email = "admin@ims.com",
                    PhoneNumber = "123456789",
                    RoleId = adminRoleId
                };
            var passwordHasher = new PasswordHasher<User>();
            user.Password=passwordHasher.HashPassword(user,adminPassword);
            context.Users.Add(user);
            context.SaveChanges();
            }
        }
    }
}