using System.Collections.Generic;
using System.Threading.Tasks;

using Epsilon.Web.ViewModels.Category;

namespace Epsilon.Services.Data.Contracts
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryCreateInputModel model, string creatorId);

        Task<List<T>> GetAllAsync<T>();

        Task<List<T>> GetAllWithDeletedAsync<T>();

        Task<T> GetOneByIdAsync<T>(int id);

        Task EditByIdAsync(CategoryEditInputModel model, string creatorId);

        Task DeleteByIdAsync(int categoryId);
    }
}
