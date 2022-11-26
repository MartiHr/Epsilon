using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            // TODO: Check if user exists

            //if (applicationUserId)
            //{

            //}

            var customer = new Customer()
            {
                ApplicationUserId = applicationUserId,
            };

            await customerRepository.AddAsync(customer);
            await customerRepository.SaveChangesAsync();
        }
    }
}
