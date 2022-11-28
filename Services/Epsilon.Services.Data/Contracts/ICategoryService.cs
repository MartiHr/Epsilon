using System.Collections.Generic;
using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface ICategoryService
    {
        Task CreateAsync(string categoryName);

        Task<List<T>> GetAllAsync<T>();
    }
}
