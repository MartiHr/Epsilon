using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Epsilon.Services.Data
{
    public class ImageService : IImageService
    {
        private readonly IDeletableEntityRepository<Image> imageRepository;

        public ImageService(IDeletableEntityRepository<Image> _imageRepository)
        {
            imageRepository = _imageRepository;
        }

        public async Task DeleteImageByIdAsync(string imageId)
        {
            var image = await GetByIdAsync(imageId);

            imageRepository.Delete(image);
            await imageRepository.SaveChangesAsync();
        }

        public async Task<Image> GetByIdAsync(string imageId)
        {
            return await imageRepository
                .All()
                .FirstOrDefaultAsync(i => i.Id == imageId);
        }

        //public Task<T> GetImagesByCreatorIdAsync<T>(string creatorId)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
