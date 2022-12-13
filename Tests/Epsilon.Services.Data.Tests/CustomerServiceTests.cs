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
    }
}
