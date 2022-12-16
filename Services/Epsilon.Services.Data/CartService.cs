using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Epsilon.Services.Mapping;
using Microsoft.EntityFrameworkCore;

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

            if (cart.Computers.Any(c => c.Id == computer.Id))
            {
                throw new ArgumentNullException("Computer already added");
            }

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

        public async Task EmptyAsync(string customerId)
        {
            var cart = await cartRepository.All().FirstOrDefaultAsync(c => c.CustomerId == customerId);

            foreach (var computer in cart.Computers)
            {
                cart.Computers.Remove(computer);
            }

            cartRepository.Update(cart);
            await cartRepository.SaveChangesAsync();
        }

        public async Task<List<Computer>> GetAllComputersFromCartByCustomerIdAsync(string customerId)
        {
            var cart = await GetCartByCustomerIdAsync(customerId);

            return cart.Computers.ToList();
        }

        public async Task<List<T>> GetAllComputersOfCustomerCartAsync<T>(string customerId)
        {
            var computers = await cartRepository
                .AllAsNoTracking()
                .Where(c => c.CustomerId == customerId)
                .Include(c => c.Computers)
                .SelectMany(c => c.Computers)
                .ToListAsync();

            var items = await cartRepository
                .AllAsNoTracking()
                .Where(c => c.CustomerId == customerId)
                .Include(c => c.Computers)
                .SelectMany(c => c.Computers)
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

        public async Task RemoveComputerFromCartAsync(int computerId, string cartId)
        {
            var cart = await cartRepository
                .All()
                .FirstOrDefaultAsync(c => c.Id == cartId);

            var computer = await computerService.GetOneByIdAsync(computerId);

            cart.Computers.Remove(computer);

            cartRepository.Update(cart);
            await cartRepository.SaveChangesAsync();
        }
    }
}
