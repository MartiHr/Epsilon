using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Epsilon.Services.Data
{
    public class CustomerService : ICustomerService
    {
        private readonly IDeletableEntityRepository<Customer> customerRepository;

        public CustomerService(IDeletableEntityRepository<Customer> _customerRepository)
        {
            customerRepository = _customerRepository;
        }

        public async Task CreateAsync(string applicationUserId)
        {
            var customer = new Customer()
            {
                ApplicationUserId = applicationUserId,
            };

            await customerRepository.AddAsync(customer);
            await customerRepository.SaveChangesAsync();
        }

        public async Task<string> GetCustomerIdAsync(string userId)
        {
            return await customerRepository
                .AllAsNoTracking()
                .Where(c => c.ApplicationUserId == userId)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasCartAsync(string customerId)
        {
            var customer = await customerRepository
                .AllAsNoTracking()
                .Where(c => c.Id == customerId)
                .Include(c => c.Cart)
                .FirstOrDefaultAsync();

            return customer.Cart != null;
        }
    }
}
