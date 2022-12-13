using System.Threading.Tasks;

using Epsilon.Data;
using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Epsilon.Services.Data.Tests
{
    public class ImageServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Image> imageRepository;
        private IDeletableEntityRepository<Editor> editorRepository;
        private IDeletableEntityRepository<ApplicationUser> applicationUserRepository;
        private ImageService imageService;

        public ImageServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("EpsilonImageDatabase")
                .Options;

            applicationDbContext = new ApplicationDbContext(contextOptions);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();

            applicationUserRepository = new EfDeletableEntityRepository<ApplicationUser>(applicationDbContext);
            editorRepository = new EfDeletableEntityRepository<Editor>(applicationDbContext);
            imageRepository = new EfDeletableEntityRepository<Image>(applicationDbContext);
            imageService = new ImageService(imageRepository);
        }

        [Fact]
        public async Task GetOneByIdAsyncWorksProperly()
        {
            await SeedDataAsync();

            var image = await imageService.GetByIdAsync("1");

            Assert.NotNull(image);
            Assert.IsType<Image>(image);
            Assert.Equal("1", image.Id);
            Assert.Equal(".jpg", image.Extension);
        }

        [Fact]
        public async Task DeleteByIdAsyncWorksProperly()
        {
            await SeedDataAsync();

            await imageService.DeleteImageByIdAsync("1");

            Assert.Empty(imageRepository.All());
        }

        private async Task SeedDataAsync()
        {
            var user = new ApplicationUser();

            await applicationUserRepository.AddAsync(user);
            await applicationUserRepository.SaveChangesAsync();

            var editor = new Editor()
            {
                ApplicationUser = user,
            };

            await editorRepository.AddAsync(editor);
            await editorRepository.SaveChangesAsync();

            var image = new Image()
            {
                Id = "1",
                Extension = ".jpg",
                Creator = editor,
            };

            await imageRepository.AddAsync(image);
            await imageRepository.SaveChangesAsync();
        }
    }
}
