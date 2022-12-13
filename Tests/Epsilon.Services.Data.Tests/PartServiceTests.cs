using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Epsilon.Data;
using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Data.Repositories;
using Epsilon.Services.Data.Contracts;
using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Manufacturer;
using Epsilon.Web.ViewModels.Part;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Epsilon.Services.Data.Tests
{
    public class PartServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Part> partRepository;
        private IDeletableEntityRepository<Manufacturer> manufacturerRepository;
        private IDeletableEntityRepository<Editor> editorRepository;
        private IDeletableEntityRepository<ApplicationUser> applicationUserRepository;
        private IDeletableEntityRepository<Category> categoryRepository;
        private IDeletableEntityRepository<Computer> computerRepository;
        private PartService partService;

        public PartServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("EpsilonManufacturerDatabase")
                .Options;

            applicationDbContext = new ApplicationDbContext(contextOptions);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();

            manufacturerRepository = new EfDeletableEntityRepository<Manufacturer>(applicationDbContext);
            partRepository = new EfDeletableEntityRepository<Part>(applicationDbContext);
            editorRepository = new EfDeletableEntityRepository<Editor>(applicationDbContext);
            applicationUserRepository = new EfDeletableEntityRepository<ApplicationUser>(applicationDbContext);
            categoryRepository = new EfDeletableEntityRepository<Category>(applicationDbContext);
            computerRepository = new EfDeletableEntityRepository<Computer>(applicationDbContext);
            partService = new PartService(partRepository);
        }

        [Fact]
        public async Task CreateAsyncWorksProperly()
        {
            await SeedDataAsync();

            var dbManufacturer = await manufacturerRepository.All().FirstOrDefaultAsync();

            var inputModel = new PartCreateInputModel()
            {
                Type = "NewPart",
                Model = "NewPartModel",
                Description = "Part description which contains an x amount of characters",
                ManufacturerId = dbManufacturer.Id,
            };

            await partService.CreateAsync(inputModel, string.Empty);

            var dbModel = await partRepository.All().FirstOrDefaultAsync(p => p.Type == "NewPart");

            Assert.NotNull(dbModel);
        }

        [Fact]
        public async Task DeleteByIdAsyncDeletesProperly()
        {
            await SeedDataAsync();

            await partService.DeleteByIdAsync(1);
            await partService.DeleteByIdAsync(2);

            var dbModels = await partRepository.All().ToListAsync();

            Assert.Empty(dbModels);
        }

        [Fact]
        public async Task DeleteByIdAsyncThrowsExceptionProperly()
        {
            await SeedDataAsync();

            await Assert.ThrowsAsync<ArgumentNullException>(async () => { await partService.DeleteByIdAsync(3); });
        }

        [Fact]
        public async Task EditByIdAsyncDeletesProperly()
        {
            await SeedDataAsync();

            var model = new PartEditInputModel()
            {
                Id = 1,
                Type = "EditedPart",
                Model = "EditedPartModel",
                Description = "EditedPart description which contains an x amount of characters",
                ManufacturerId = 2,

            };

            await partService.EditByIdAsync(model, string.Empty);

            var dbModel = await partRepository.All().FirstOrDefaultAsync(c => c.Id == 1);

            Assert.NotNull(dbModel);
            Assert.Equal(model.Type, dbModel.Type);
            Assert.Equal(model.Model, dbModel.Model);
            Assert.Equal(model.Description, dbModel.Description);
            Assert.Equal(model.ManufacturerId, dbModel.ManufacturerId);
        }

        [Fact]
        public async Task EditByIdAsyncThrowsExceptionProperly()
        {
            await SeedDataAsync();

            var model = new PartEditInputModel()
            {
                Id = 10,
                Type = "EditedPart",
                Model = "EditedPartModel",
                Description = "EditedPart description which contains an x amount of characters",
                ManufacturerId = 2,
            };

            await Assert.ThrowsAsync<ArgumentNullException>(async () => { await partService.EditByIdAsync(model, string.Empty); });
        }

        [Fact]
        public async Task GetAllAsyncWorksProperly()
        {
            await SeedDataAsync();

            AutoMapperConfig.RegisterMappings(typeof(PartInListViewModel).GetTypeInfo().Assembly);

            var partModels = await partService.GetAllAsync<PartInListViewModel>();

            Assert.NotEmpty(partModels);

            Assert.Equal(2, partModels.Count);

            Assert.Equal(1, partModels[0].Id);
            Assert.Equal("Part", partModels[0].Type);
            Assert.Equal("PartModel", partModels[0].Model);
            Assert.Equal("Part description which contains an x amount of characters", partModels[0].Description);
            Assert.Equal("Manufacturer", partModels[0].Manufacturer);

            Assert.Equal(2, partModels[1].Id);
            Assert.Equal("Part2", partModels[1].Type);
            Assert.Equal("PartModel2", partModels[1].Model);
            Assert.Equal("Part2 description which contains an x amount of characters", partModels[1].Description);
            Assert.Equal("Manufacturer2", partModels[1].Manufacturer);
        }

        [Fact]
        public async Task GetAllWithDeletedAsyncWorksProperly()
        {
            await SeedDataAsync();

            AutoMapperConfig.RegisterMappings(typeof(PartInListViewModel).GetTypeInfo().Assembly);

            var partModels = await partService.GetAllWithDeletedAsync<PartInListViewModel>();

            Assert.NotEmpty(partModels);
            Assert.Equal(3, partModels.Count);

            Assert.Equal(1, partModels.FirstOrDefault().Id);
        }

        [Fact]
        public async Task GetOneByIdAsyncWorksProperly()
        {
            await SeedDataAsync();

            AutoMapperConfig.RegisterMappings(typeof(PartInListViewModel).GetTypeInfo().Assembly);

            var partModel = await partService.GetOneByIdAsync<PartInListViewModel>(1);

            Assert.NotNull(partModel);
            Assert.Equal(1, partModel.Id);
            Assert.Equal("Part", partModel.Type);
            Assert.Equal("PartModel", partModel.Model);
            Assert.Equal("Part description which contains an x amount of characters", partModel.Description);
            Assert.Equal("Manufacturer", partModel.Manufacturer);
        }

        [Fact]
        public async Task GetAllOfTypeAsyncWorksProperly()
        {
            await SeedDataAsync();

            AutoMapperConfig.RegisterMappings(typeof(PartInListViewModel).GetTypeInfo().Assembly);

            var partModels = await partService.GetAllOfTypeAsync<PartInListViewModel>("Part");

            Assert.Single(partModels);
        }

        [Fact]
        public async Task AssignComputerToExistingPartAsyncWorksProperly()
        {
            await SeedDataAsync();

            var dbComputer = await computerRepository.All().FirstOrDefaultAsync();

            await partService.AssignComputerToExistingPartAsync(dbComputer, 1);

            var dbPart = await partRepository.All().FirstOrDefaultAsync(p => p.Id == 1);

            Assert.Equal(1, dbPart.Computers.Count);
        }

        [Fact]
        public async Task RemoveComputerFromExistingPartAsyncWorksProperly()
        {
            await SeedDataAsync();

            var dbComputer = await computerRepository.All().FirstOrDefaultAsync();

            await partService.RemoveComputerFromExistingPartAsync(dbComputer, 1);

            var dbPart = await partRepository.All().FirstOrDefaultAsync(p => p.Id == 1);

            Assert.Empty(dbPart.Computers);
        }

        private async Task SeedDataAsync()
        {
            var user = new ApplicationUser()
            {
                Id = "1",
            };

            await applicationUserRepository.AddAsync(user);
            await applicationUserRepository.SaveChangesAsync();

            var editor = new Editor()
            {
                Id = "1",
                ApplicationUser = user,
            };

            await editorRepository.AddAsync(editor);
            await editorRepository.SaveChangesAsync();

            var category = new Category()
            {
                Id = 1,
                Name = "Category",
            };

            await categoryRepository.AddAsync(category);
            await categoryRepository.SaveChangesAsync();

            var manufacturer = new Manufacturer()
            {
                Id = 1,
                Name = "Manufacturer",
                Country = "Country",
            };

            await manufacturerRepository.AddAsync(manufacturer);

            var manufacturer2 = new Manufacturer()
            {
                Id = 2,
                Name = "Manufacturer2",
                Country = "Country2",
            };

            await manufacturerRepository.AddAsync(manufacturer2);

            await manufacturerRepository.SaveChangesAsync();

            var part = new Part()
            {
                Id = 1,
                Type = "Part",
                Model = "PartModel",
                Description = "Part description which contains an x amount of characters",
                ManufacturerId = manufacturer.Id,
            };

            await partRepository.AddAsync(part);

            var part2 = new Part()
            {
                Id = 2,
                Type = "Part2",
                Model = "PartModel2",
                Description = "Part2 description which contains an x amount of characters",
                ManufacturerId = manufacturer2.Id,
            };

            await partRepository.AddAsync(part2);

            var part3 = new Part()
            {
                Id = 3,
                Type = "Part3",
                Model = "PartModel3",
                Description = "Part3 description which contains an x amount of characters",
                ManufacturerId = manufacturer2.Id,
                IsDeleted = true,
            };

            await partRepository.AddAsync(part3);

            await partRepository.SaveChangesAsync();

            var computer = new Computer()
            {
                Id = 1,
                Model = "Model",
                Price = 0,
                Description = "Description",
                CreatorId = "1",
                CategoryId = 1,
                ManufacturerId = 1,
            };

            await computerRepository.AddAsync(computer);
            await computerRepository.SaveChangesAsync();
        }
    }
}
