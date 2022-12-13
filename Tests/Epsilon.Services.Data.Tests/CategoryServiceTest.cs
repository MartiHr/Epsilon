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
    public class CategoryServiceTest
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Category> categoriesRepository;
        private CategoryService categoryService;

        public CategoryServiceTest()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MySportsClubManagerDbTrainings")
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
            await SeedData();

            AutoMapperConfig.RegisterMappings(typeof(CategoryInListViewModel).GetTypeInfo().Assembly);

            var category = new Category()
            {
                Id = 1,
                Name = "Category",
            };

            await categoriesRepository.AddAsync(category);
            await categoriesRepository.SaveChangesAsync();

            var categoryModel = await this.categoryService.GetOneByIdAsync<CategoryInListViewModel>(1);

            Assert.Equal(1, categoryModel.Id);
            Assert.Equal("Category", categoryModel.Name);
        }

        private async Task SeedData()
        {

        }
    }
}
