using Epsilon.Web.ViewModels.Computer;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface IComputerService
    {
        // TODO: implement
        Task<List<T>> GetAllAsync<T>(int page, int itemsPerPage);

        Task CreateAsync(ComputerCreateInputModel model, string creatorId);

        int GetCount();
    }
}
