using System.Collections.Generic;
using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface IManufacturerService
    {
        Task<List<T>> GetAllAsync<T>();
    }
}
