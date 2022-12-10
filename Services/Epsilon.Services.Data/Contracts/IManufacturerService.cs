using System.Collections.Generic;
using System.Threading.Tasks;

using Epsilon.Web.ViewModels.Manufacturer;

namespace Epsilon.Services.Data.Contracts
{
    public interface IManufacturerService
    {
        Task<List<T>> GetAllAsync<T>();

        Task<List<T>> GetAllWithDeletedAsync<T>();

        Task<T> GetOneByIdAsync<T>(int id);

        Task CreateAsync(ManufacturerCreateInputModel model, string creatorId);

        Task EditByIdAsync(ManufacturerEditInputModel model, string creatorId);

        Task DeleteByIdAsync(int manufacturerId);
    }
}
