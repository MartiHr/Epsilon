using Epsilon.Services.Data.Contracts;
using System.Threading.Tasks;

namespace Epsilon.Services.Data
{
    public class ImageService : IImageService
    {
        public Task<T> GetImagesByCreatorIdAsync<T>(string creatorId)
        {
            throw new System.NotImplementedException();
        }
    }
}
