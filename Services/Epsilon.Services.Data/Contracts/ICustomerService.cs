using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface ICustomerService
    {
        Task CreateAsync(string applicationUserId);

        Task<bool> HasCartAsync(string customerId);

        Task<string> GetCustomerIdAsync(string userId);
    }
}
