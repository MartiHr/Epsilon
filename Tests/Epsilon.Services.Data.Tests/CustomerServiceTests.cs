using System.Threading.Tasks;

using Epsilon.Data;
using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Epsilon.Services.Data.Tests
{
    public class CustomerServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Customer> customerRepository;
        private IDeletableEntityRepository<ApplicationUser> applicationUserRepository;
        private IDeletableEntityRepository<Cart> cartRepository;
        private CustomerService customerService;

        public CustomerServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("EpsilonCustomerDatabase")
                .Options;

            applicationDbContext = new ApplicationDbContext(contextOptions);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();

            customerRepository = new EfDeletableEntityRepository<Customer>(applicationDbContext);
            applicationUserRepository = new EfDeletableEntityRepository<ApplicationUser>(applicationDbContext);
            cartRepository = new EfDeletableEntityRepository<Cart>(applicationDbContext);
            customerService = new CustomerService(customerRepository);
        }

        [Fact]
        public async Task CreateAsyncWorksProperly()
        {
            var user = new ApplicationUser();

            await applicationUserRepository.AddAsync(user);
            await applicationUserRepository.SaveChangesAsync();

            await customerService.CreateAsync(user.Id);

            var dbModel = await customerRepository.All().FirstOrDefaultAsync();

            Assert.NotEmpty(customerRepository.All());
            Assert.NotNull(dbModel);
            Assert.Equal(user.Id, dbModel.ApplicationUser.Id);
        }

        [Fact]
        public async Task GetCustomerIdAsyncWorksProperly()
        {
            var user = new ApplicationUser();

            await applicationUserRepository.AddAsync(user);
            await applicationUserRepository.SaveChangesAsync();

            var customer = new Customer()
            {
                ApplicationUserId = user.Id,
            };

            await customerRepository.AddAsync(customer);
            await customerRepository.SaveChangesAsync();

            var resultId = await customerService.GetCustomerIdAsync(user.Id);

            Assert.Equal(customer.Id, resultId);
        }

        [Fact]
        public async Task HasCartAsyncWorksProperly()
        {
            var user = new ApplicationUser();

            await applicationUserRepository.AddAsync(user);
            await applicationUserRepository.SaveChangesAsync();

            var customer = new Customer()
            {
                ApplicationUserId = user.Id,
            };

            await customerRepository.AddAsync(customer);
            await customerRepository.SaveChangesAsync();

            var cart = new Cart()
            {
                CustomerId = customer.Id,
            };

            await cartRepository.AddAsync(cart);
            await cartRepository.SaveChangesAsync();

            var result = await customerService.HasCartAsync(customer.Id);

            Assert.True(result);
        }
    }
}
