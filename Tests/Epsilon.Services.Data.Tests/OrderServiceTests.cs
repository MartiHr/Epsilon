using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Data.Repositories;
using Epsilon.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epsilon.Services.Data.Contracts;
using Xunit;
using Epsilon.Web.ViewModels.Order;
using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Part;
using System.Reflection;
using Epsilon.Web.ViewModels.Computer;

namespace Epsilon.Services.Data.Tests
{
    public class OrderServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Part> partRepository;
        private IDeletableEntityRepository<Image> imageRepository;
        private IDeletableEntityRepository<Computer> computerRepository;
        private IDeletableEntityRepository<Order> orderRepository;
        private IDeletableEntityRepository<Customer> customerRepository;
        private IDeletableEntityRepository<ApplicationUser> applicationUserRepository;
        private IDeletableEntityRepository<Cart> cartRepository;
        private IDeletableEntityRepository<Manufacturer> manufacturerRepository;
        private IDeletableEntityRepository<Category> categoryRepository;
        private IDeletableEntityRepository<Editor> editorRepository;
        private PartService partService;
        private ImageService imageService;
        private ComputerService computerService;
        private OrderService orderService;

        public OrderServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("EpsilonOrderDatabase")
                .Options;

            applicationDbContext = new ApplicationDbContext(contextOptions);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();

            partRepository = new EfDeletableEntityRepository<Part>(applicationDbContext);
            customerRepository = new EfDeletableEntityRepository<Customer>(applicationDbContext);
            applicationUserRepository = new EfDeletableEntityRepository<ApplicationUser>(applicationDbContext);
            cartRepository = new EfDeletableEntityRepository<Cart>(applicationDbContext);
            manufacturerRepository = new EfDeletableEntityRepository<Manufacturer>(applicationDbContext);
            categoryRepository = new EfDeletableEntityRepository<Category>(applicationDbContext);
            editorRepository = new EfDeletableEntityRepository<Editor>(applicationDbContext);
            computerRepository = new EfDeletableEntityRepository<Computer>(applicationDbContext);
            orderRepository = new EfDeletableEntityRepository<Order>(applicationDbContext);
            imageRepository = new EfDeletableEntityRepository<Image>(applicationDbContext);

            partService = new PartService(partRepository);
            imageService = new ImageService(imageRepository);
            computerService = new ComputerService(computerRepository, partService, imageService);
            orderService = new OrderService(orderRepository, computerService);
        }

        [Fact]
        public async Task AddComputerToOrderAsyncWorksProperly()
        {
            await SeedDataAsync();

            var computer = await computerRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == 1);

            await orderService.AddComputerToOrderAsync("1", 1);

            var order = await orderRepository
                .AllAsNoTracking()
                .Where(o => o.Id == "1")
                .Include(o => o.Computers)
                .FirstOrDefaultAsync();

            Assert.Single(order.Computers);
        }

        [Fact]
        public async Task CreateAsyncWorksProperly()
        {
            await orderService.CreateAsync("1", "NewAddress");

            var orders = await orderRepository
                .AllAsNoTracking()
                .Where(o => o.CustomerId == "1")
                .ToListAsync();

            Assert.Single(orders);
        }

        [Fact]
        public async Task GetAllAsyncWorksProperly()
        {
            await SeedDataAsync();

            AutoMapperConfig.RegisterMappings(typeof(OrderInListViewModel).GetTypeInfo().Assembly);

            var orders = await orderService.GetAllAsync<OrderInListViewModel>("1");

            Assert.Single(orders);
        }

        [Fact]
        public async Task GetAllComputersOfOrderAsyncWorksProperly()
        {
            await SeedDataAsync();

            AutoMapperConfig.RegisterMappings(typeof(ComputerInListViewModel).GetTypeInfo().Assembly);

            var orders = await orderService.GetAllComputersOfOrderAsync<ComputerInListViewModel>("1", "1");

            Assert.Empty(orders);
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

            var customer = new Customer()
            {
                Id = "1",
                ApplicationUser = user,
            };

            await customerRepository.AddAsync(customer);
            await customerRepository.SaveChangesAsync();

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

            var order = new Order()
            {
                Id = "1",
                CustomerId = "1",
                Address = "Address",
            };

            await orderRepository.AddAsync(order);
            await orderRepository.SaveChangesAsync();
        }
    }
}
