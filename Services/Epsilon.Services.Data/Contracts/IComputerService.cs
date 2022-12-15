using System.Collections.Generic;
using System.Threading.Tasks;
using Epsilon.Data.Models;
using Epsilon.Web.ViewModels.Computer;

namespace Epsilon.Services.Data.Contracts
{
    public interface IComputerService
    {
        // TODO: implement
        Task<List<T>> GetAllAsync<T>(int page, int itemsPerPage);

        Task<List<T>> GetANumberOfAsync<T>(int count);

        Task<Computer> GetOneByIdAsync(int id);

        Task CreateAsync(ComputerCreateInputModel model, string creatorId, string imagePath);

        Task EditByIdAsync(ComputerEditInputModel model, string creatorId, string imagePath);

        Task<T> GetByIdAsync<T>(int id);

        Task DeleteAsync(int id);

        int GetCount();
    }
}
