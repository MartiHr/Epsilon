using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface IImageService
    {
        Task<T> GetImagesByCreatorIdAsync<T>(string creatorId);
    }
}
