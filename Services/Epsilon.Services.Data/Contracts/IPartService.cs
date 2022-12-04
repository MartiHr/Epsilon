using Epsilon.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface IPartService
    {
        Task<List<T>> GetAllAsync<T>();

        Task<List<T>> GetAllOfTypeAsync<T>(string type);

        Task AssignComputerToPart(Computer computer, int partId);
    }
}
