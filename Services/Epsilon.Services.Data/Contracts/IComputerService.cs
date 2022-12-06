using System.Collections.Generic;
using System.Threading.Tasks;

using Epsilon.Web.ViewModels.Computer;

namespace Epsilon.Services.Data.Contracts
{
    public interface IComputerService
    {
        // TODO: implement
        Task<List<T>> GetAllAsync<T>(int page, int itemsPerPage);

        Task CreateAsync(ComputerCreateInputModel model, string creatorId, string imagePath);

        Task<T> GetByIdAsync<T>(int id);

        int GetCount();
    }
}
