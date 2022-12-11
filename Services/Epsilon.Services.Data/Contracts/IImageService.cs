using Epsilon.Data.Models;
using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface IImageService
    {
        Task<T> GetImagesByCreatorIdAsync<T>(string creatorId);

        Task<Image> GetByIdAsync(string imageId);

        Task DeleteImageByIdAsync(string imageId);
    }
}
