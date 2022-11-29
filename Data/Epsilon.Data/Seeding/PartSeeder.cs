using System;
using System.Linq;
using System.Threading.Tasks;

using Epsilon.Data.Models;

namespace Epsilon.Data.Seeding
{
    public class PartSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Parts.Any())
            {
                return;
            }

            await dbContext.Parts.AddAsync(new Part()
            {
                Type = "CPU",
                Model = "Ryzen 3",
                Description = "Low-end processor",
                ManufacturerId = 1,
            });

            await dbContext.Parts.AddAsync(new Part()
            {
                Type = "CPU",
                Model = "Ryzen 5",
                Description = "Mid-range processor",
                ManufacturerId = 3,
            });

            await dbContext.Parts.AddAsync(new Part()
            {
                Type = "CPU",
                Model = "Ryzen 7",
                Description = "High-end processor",
                ManufacturerId = 2,
            });

            await dbContext.Parts.AddAsync(new Part()
            {
                Type = "GPU",
                Model = "Nvidia RTX 4090",
                Description = "The ultimate graphics card",
                ManufacturerId = 1,
            });

            await dbContext.Parts.AddAsync(new Part()
            {
                Type = "Storage",
                Model = "Samsung Evo 960",
                Description = "Hyper fast SSD",
                ManufacturerId = 1,
            });

            await dbContext.Parts.AddAsync(new Part()
            {
                Type = "Storage",
                Model = "Samsung Evo 960",
                Description = "Hyper fast SSD",
                ManufacturerId = 1,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
