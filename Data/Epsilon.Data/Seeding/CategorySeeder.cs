using Epsilon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Data.Seeding
{
    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category() { Name = "Gaming" });
            await dbContext.Categories.AddAsync(new Category() { Name = "Office" });
            await dbContext.Categories.AddAsync(new Category() { Name = "All-in-One" });

            await dbContext.SaveChangesAsync();
        }
    }
}
