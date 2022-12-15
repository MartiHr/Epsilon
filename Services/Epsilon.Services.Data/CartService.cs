using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Epsilon.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Services.Data
{
    public class CartService : ICartService
    {

        private readonly IDeletableEntityRepository<Cart> cartRepository;
        private readonly IComputerService computerService;

        public CartService(IDeletableEntityRepository<Cart> _cartRepository, IComputerService _computerService)
        {
            cartRepository = _cartRepository;
            computerService = _computerService;
        }

        public async Task AddComputerToCartAsync(int computerId, string cartId)
        {
            var cart = await cartRepository
                .All()
                .FirstOrDefaultAsync(c => c.Id == cartId);

            var computer = await computerService.GetOneByIdAsync(computerId);

            cart.Computers.Add(computer);

            cartRepository.Update(cart);
            await cartRepository.SaveChangesAsync();
        }

        public async Task CreateCartAsync(string customerId)
        {
            var cart = new Cart()
            {
                CustomerId = customerId,
            };

            await cartRepository.AddAsync(cart);
            await cartRepository.SaveChangesAsync();
        }

        public async Task<List<Computer>> GetAllComputersFromCartByCustomerIdAsync(string customerId)
        {
            var cart = await GetCartByCustomerIdAsync(customerId);

            return cart.Computers.ToList();
        }

        public async Task<List<T>> GetAllComputersOfCustomerAsync<T>(string customerId)
        {
            var items = await cartRepository
                .AllAsNoTracking()
                .Where(c => c.CustomerId == customerId)
                .To<T>()
                .ToListAsync();

            return items;
        }

        public async Task<Cart> GetCartByCustomerIdAsync(string customerId)
        {
            return await cartRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }
    }
}
