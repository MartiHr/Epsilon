using Epsilon.Data;
using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Data.Repositories;
using Epsilon.Services.Data.Contracts;
using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Computer;
using Epsilon.Web.ViewModels.Part;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Epsilon.Services.Data.Tests
{
    public class ComputerServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Part> partRepository;
        private IDeletableEntityRepository<Manufacturer> manufacturerRepository;
        private IDeletableEntityRepository<Editor> editorRepository;
        private IDeletableEntityRepository<ApplicationUser> applicationUserRepository;
        private IDeletableEntityRepository<Category> categoryRepository;
        private IDeletableEntityRepository<Computer> computerRepository;
        private ComputerService computerService;
        private PartService partService;
        private ImageService imageService;

        public ComputerServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("EpsilonComputerDatabase")
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
            computerService = new ComputerService(computerRepository, partService, imageService);
        }

        [Fact]
        public async Task GetCountWorksProperly()
        {
            await SeedDataAsync();

            var count = computerService.GetCount();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task GetOneByIdAsyncWorksProperly()
        {
            await SeedDataAsync();

            var computer = await computerService.GetOneByIdAsync(1);

            Assert.NotNull(computer);
            Assert.Equal("Model", computer.Model);
        }

        [Fact]
        public async Task GetByIdAsyncGenericWorksProperly()
        {
            AutoMapperConfig.RegisterMappings(typeof(ComputerInListViewModel).GetTypeInfo().Assembly);

            await SeedDataAsync();

            var computer = await computerService.GetByIdAsync<ComputerInListViewModel>(1);

            Assert.NotNull(computer);
            Assert.Equal(0, computer.Price);
        }

        [Fact]
        public async Task GetByIdAsyncGenericThrowsExceptionProperly()
        {
            AutoMapperConfig.RegisterMappings(typeof(ComputerInListViewModel).GetTypeInfo().Assembly);

            await SeedDataAsync();

            await Assert.ThrowsAsync<Exception>(async () => { await computerService.GetByIdAsync<ComputerInListViewModel>(10); });
        }

        [Fact]
        public async Task GetANumberOfAsyncWorksProperly()
        {
            AutoMapperConfig.RegisterMappings(typeof(ComputerInListViewModel).GetTypeInfo().Assembly);

            await SeedDataAsync();

            var computers = await computerService.GetANumberOfAsync<ComputerInListViewModel>(1);

            Assert.Single(computers);
        }

        [Fact]
        public async Task GetAllAsyncWorksProperly()
        {
            AutoMapperConfig.RegisterMappings(typeof(ComputerInListViewModel).GetTypeInfo().Assembly);

            await SeedDataAsync();

            var computers = await computerService.GetAllAsync<ComputerInListViewModel>(1, 1);

            Assert.Single(computers);
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

            var cpu = new Part()
            {
                Id = 4,
                Type = "CPU",
                Model = "PartModel4",
                Description = "Part4 description which contains an x amount of characters",
                ManufacturerId = manufacturer2.Id,
            };

            await partRepository.AddAsync(cpu);

            var gpu = new Part()
            {
                Id = 5,
                Type = "GPU",
                Model = "PartModel5",
                Description = "Part5 description which contains an x amount of characters",
                ManufacturerId = manufacturer2.Id,
            };

            await partRepository.AddAsync(gpu);

            var storage = new Part()
            {
                Id = 6,
                Type = "Storage",
                Model = "PartModel6",
                Description = "Part6 description which contains an x amount of characters",
                ManufacturerId = manufacturer2.Id,
            };

            await partRepository.AddAsync(storage);

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
                Parts = new List<Part> { cpu, gpu, storage },
            };

            await computerRepository.AddAsync(computer);

            await computerRepository.SaveChangesAsync();

            cpu.Computers.Add(computer);
            gpu.Computers.Add(computer);
            storage.Computers.Add(computer);

            await partRepository.SaveChangesAsync();
        }
    }
}
