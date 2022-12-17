using System.Collections.Generic;
using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface IOrderService
    {
        Task<string> CreateAsync(string customerId, string address);

        Task AddComputerToOrderAsync(string orderId, int computerId);

        Task<List<T>> GetAllAsync<T>(string customerId);

        Task<List<T>> GetAllComputersOfOrderAsync<T>(string customerId, string orderId);
    }
}
