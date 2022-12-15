using Epsilon.Data.Models;
using Epsilon.Web.ViewModels.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface ICartService
    {
        Task<Cart> GetCartByCustomerIdAsync(string customerId);

        //Task<List<Computer>> GetAllComputersFromCartByCustomerIdAsync<Computer>(string customerId);

        Task CreateCartAsync(string customerId);

        Task AddComputerToCartAsync(int computerId, string cartId);

        Task<List<T>> GetAllComputersOfCustomerAsync<T>(string customerId);
    }
}
