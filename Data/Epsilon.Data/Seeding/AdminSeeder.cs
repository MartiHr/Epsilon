using Epsilon.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Epsilon.Data.Seeding
{
    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any(u => u.UserName == "admin"))
            {
                return;
            }

            var admin = new ApplicationUser()
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                PhoneNumber = "+359881230123",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "123123");

            var userStore = new UserStore<ApplicationUser>(dbContext);
            var result = await userStore.CreateAsync(admin);

            var adminFromDb = await dbContext.Users.Where(x => x.UserName == "admin").FirstOrDefaultAsync();

            if (result.Succeeded)
            {
                var customer = new Customer()
                {
                    ApplicationUserId = adminFromDb.Id,
                };

                await dbContext.Customers.AddAsync(customer);

                var editor = new Editor()
                {
                    ApplicationUserId = adminFromDb.Id,
                };

                await dbContext.Editors.AddAsync(editor);

                var role = await dbContext.Roles.Where(r => r.Name == "Administrator").FirstOrDefaultAsync();

                await dbContext.UserRoles.AddAsync(new IdentityUserRole<string>()
                {
                    UserId = adminFromDb.Id,
                    RoleId = role.Id,
                });

                await dbContext.SaveChangesAsync();
            }

            return;
        }
    }
}
