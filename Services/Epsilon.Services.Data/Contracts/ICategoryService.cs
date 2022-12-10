using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Computer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryCreateInputModel model, string creatorId);

        Task<List<T>> GetAllAsync<T>();

        Task<T> GetOneByIdAsync<T>(int id);

        Task EditByIdAsync(CategoryEditInputModel model, string creatorId);
    }
}
