using System;
using System.Linq;
using System.Threading.Tasks;

using Epsilon.Data.Models;

namespace Epsilon.Data.Seeding
{
    public class ManufacturerSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Manufacturers.Any())
            {
                return;
            }

            await dbContext.Manufacturers.AddAsync(new Manufacturer() { Name = "Asus", Country = "Gaindward" });
            await dbContext.Manufacturers.AddAsync(new Manufacturer() { Name = "Asus", Country = "USA" });
            await dbContext.Manufacturers.AddAsync(new Manufacturer() { Name = "Acer", Country = "USA" });

            await dbContext.SaveChangesAsync();
        }
    }
}
