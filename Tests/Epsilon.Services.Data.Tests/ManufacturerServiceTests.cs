using Epsilon.Data;
using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Data.Repositories;
using Epsilon.Services.Data.Contracts;
using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Manufacturer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Epsilon.Services.Data.Tests
{
    public class ManufacturerServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Manufacturer> manufacturerRepository;
        private ManufacturerService manufacturerService;

        public ManufacturerServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("EpsilonManufacturerDatabase")
                .Options;

            applicationDbContext = new ApplicationDbContext(contextOptions);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();

            manufacturerRepository = new EfDeletableEntityRepository<Manufacturer>(applicationDbContext);
            manufacturerService = new ManufacturerService(manufacturerRepository);
        }

        [Fact]
        public async Task CreateAsyncWorksProperly()
        {
            var model = new ManufacturerCreateInputModel()
            {
                Name = "New manufacturer",
                Country = "New country",
            };

            await manufacturerService.CreateAsync(model, string.Empty);

            var dbModel = await manufacturerRepository.All().FirstOrDefaultAsync();

            Assert.Equal(model.Name, dbModel.Name);
            Assert.Equal(model.Country, dbModel.Country);
        }

        [Fact]
        public async Task DeleteByIdAsyncDeletesProperly()
        {
            await SeedDataAsync();

            await manufacturerService.DeleteByIdAsync(1);

            var dbModels = await manufacturerRepository.All().ToListAsync();

            Assert.Empty(dbModels);
        }

        [Fact]
        public async Task DeleteByIdAsyncThrowsExceptionProperly()
        {
            await SeedDataAsync();

            await Assert.ThrowsAsync<ArgumentNullException>(async () => { await manufacturerService.DeleteByIdAsync(3); });
        }

        [Fact]
        public async Task EditByIdAsyncDeletesProperly()
        {
            await SeedDataAsync();

            var model = new ManufacturerEditInputModel()
            {
                Id = 1,
                Name = "Edited category",
                Country = "Edited country",
            };

            await manufacturerService.EditByIdAsync(model, string.Empty);

            var dbModel = await manufacturerRepository.All().FirstOrDefaultAsync(c => c.Id == model.Id);

            Assert.Equal(model.Name, dbModel.Name);
            Assert.Equal(model.Country, dbModel.Country);
        }

        [Fact]
        public async Task EditByIdAsyncThrowsExceptionProperly()
        {
            await SeedDataAsync();

            var model = new ManufacturerEditInputModel()
            {
                Id = 10,
                Name = "Edited category",
                Country = "Edited country",
            };

            await Assert.ThrowsAsync<ArgumentNullException>(async () => { await manufacturerService.EditByIdAsync(model, string.Empty); });
        }

        [Fact]
        public async Task GetOneByIdAsyncWorksProperly()
        {
            await SeedDataAsync();

            AutoMapperConfig.RegisterMappings(typeof(ManufacturerInListViewModel).GetTypeInfo().Assembly);

            var manufacturerModel = await manufacturerService.GetOneByIdAsync<ManufacturerInListViewModel>(1);

            Assert.NotNull(manufacturerModel);
            Assert.Equal(1, manufacturerModel.Id);
            Assert.Equal("Manufacturer", manufacturerModel.Name);
            Assert.Equal("Country", manufacturerModel.Country);
        }

        [Fact]
        public async Task GetAllWithDeletedAsyncWorksProperly()
        {
            await SeedDataAsync();

            AutoMapperConfig.RegisterMappings(typeof(ManufacturerInListViewModel).GetTypeInfo().Assembly);

            var manufacturerModels = await manufacturerService.GetAllWithDeletedAsync<ManufacturerInListViewModel>();

            Assert.Equal(1, manufacturerModels[0].Id);
            Assert.Equal("Manufacturer", manufacturerModels[0].Name);
            Assert.Equal("Country", manufacturerModels[0].Country);

            Assert.Equal(2, manufacturerModels[1].Id);
            Assert.Equal("Manufacturer2", manufacturerModels[1].Name);
            Assert.Equal("Country2", manufacturerModels[1].Country);
            Assert.True(manufacturerModels[1].IsDeleted);

            Assert.Equal(2, manufacturerModels.Count);
        }

        [Fact]
        public async Task GetAllAsyncWorksProperly()
        {
            await SeedDataAsync();

            AutoMapperConfig.RegisterMappings(typeof(ManufacturerInListViewModel).GetTypeInfo().Assembly);

            var manufacturerModels = await manufacturerService.GetAllAsync<ManufacturerInListViewModel>();

            Assert.Equal(1, manufacturerModels[0].Id);
            Assert.Equal("Manufacturer", manufacturerModels[0].Name);
            Assert.Equal("Country", manufacturerModels[0].Country);

            Assert.Single(manufacturerModels);
        }

        private async Task SeedDataAsync()
        {
            var manufacturer = new Manufacturer()
            {
                Id = 1,
                Name = "Manufacturer",
                Country = "Country",
            };

            await manufacturerRepository.AddAsync(manufacturer);

            var delManufacturer = new Manufacturer()
            {
                Id = 2,
                Name = "Manufacturer2",
                Country = "Country2",
                IsDeleted = true,
            };

            await manufacturerRepository.AddAsync(delManufacturer);

            await manufacturerRepository.SaveChangesAsync();
        }
    }
}
