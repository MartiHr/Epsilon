using System.Collections.Generic;
using System.Threading.Tasks;

using Epsilon.Data.Models;

namespace Epsilon.Services.Data.Contracts
{
    public interface IPartService
    {
        Task<List<T>> GetAllAsync<T>();

        Task<List<T>> GetAllOfTypeAsync<T>(string type);

        Task<List<T>> GetAllWithDeletedAsync<T>();

        Task AssignComputerToExistingPartAsync(Computer computer, int partId);
    }
}
