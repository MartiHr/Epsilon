using System;
using System.Reflection;
using System.Threading.Tasks;

using Epsilon.Data;
using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Data.Repositories;
using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Epsilon.Services.Data.Tests
{
    public class CategoryServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Category> categoriesRepository;
        private CategoryService categoryService;

        public CategoryServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("EpsilonCategoryDatabase")
                .Options;

            applicationDbContext = new ApplicationDbContext(contextOptions);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();

            categoriesRepository = new EfDeletableEntityRepository<Category>(applicationDbContext);
            categoryService = new CategoryService(categoriesRepository);
        }

        [Fact]
        public async Task GetOneByIdAsyncWorksProperly()
        {
            await SeedDataAsync();

            AutoMapperConfig.RegisterMappings(typeof(CategoryInListViewModel).GetTypeInfo().Assembly);

            var categoryModel = await categoryService.GetOneByIdAsync<CategoryInListViewModel>(1);

            Assert.Equal(1, categoryModel.Id);
            Assert.Equal("Category", categoryModel.Name);
        }

        [Fact]
        public async Task GetAllWithDeletedAsyncWorksProperly()
        {
            await SeedDataAsync();

            AutoMapperConfig.RegisterMappings(typeof(CategoryInListViewModel).GetTypeInfo().Assembly);

            var categoryModels = await categoryService.GetAllWithDeletedAsync<CategoryInListViewModel>();

            Assert.Equal(1, categoryModels[0].Id);
            Assert.Equal("Category", categoryModels[0].Name);

            Assert.Equal(2, categoryModels[1].Id);
            Assert.Equal("Category2", categoryModels[1].Name);
            Assert.True(categoryModels[1].IsDeleted);
        }

        [Fact]
        public async Task GetAllAsyncWorksProperly()
        {
            await SeedDataAsync();

            AutoMapperConfig.RegisterMappings(typeof(CategoryInListViewModel).GetTypeInfo().Assembly);

            var categoryModels = await categoryService.GetAllAsync<CategoryInListViewModel>();

            Assert.Equal(1, categoryModels[0].Id);
            Assert.Equal("Category", categoryModels[0].Name);

            Assert.Single(categoryModels);
        }

        [Fact]
        public async Task CreateAsyncWorksProperly()
        {
            var model = new CategoryCreateInputModel()
            {
                Name = "NewCategory",
            };

            await categoryService.CreateAsync(model, string.Empty);

            var dbModel = await categoriesRepository.All().FirstOrDefaultAsync();

            Assert.Equal(model.Name, dbModel.Name);
        }

        [Fact]
        public async Task DeleteByIdAsyncDeletesProperly()
        {
            await SeedDataAsync();

            await categoryService.DeleteByIdAsync(1);

            var dbModels = await categoriesRepository.All().ToListAsync();

            Assert.Empty(dbModels);
        }

        [Fact]
        public async Task DeleteByIdAsyncThrowsExceptionProperly()
        {
            await SeedDataAsync();

            await Assert.ThrowsAsync<ArgumentNullException>(async () => { await categoryService.DeleteByIdAsync(3); });
        }

        [Fact]
        public async Task EditByIdAsyncDeletesProperly()
        {
            await SeedDataAsync();

            var model = new CategoryEditInputModel()
            {
                Id = 1,
                Name = "Edited category",
            };

            await categoryService.EditByIdAsync(model, string.Empty);

            var dbModel = await categoriesRepository.All().FirstOrDefaultAsync(c => c.Name == "Edited category");

            Assert.NotNull(dbModel);
        }

        [Fact]
        public async Task EditByIdAsyncThrowsExceptionProperly()
        {
            await SeedDataAsync();

            var model = new CategoryEditInputModel()
            {
                Id = 3,
                Name = "Edited category",
            };

            await Assert.ThrowsAsync<ArgumentNullException>(async () => { await categoryService.EditByIdAsync(model, string.Empty); });
        }

        private async Task SeedDataAsync()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "Category",
            };

            await categoriesRepository.AddAsync(category);

            var delCategory = new Category()
            {
                Id = 2,
                Name = "Category2",
                IsDeleted = true,
            };

            await categoriesRepository.AddAsync(delCategory);

            await categoriesRepository.SaveChangesAsync();
        }
    }
}
