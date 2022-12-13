using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Repositories;
using Epsilon.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Epsilon.Data.Models;

namespace Epsilon.Services.Data.Tests
{
    public class EditorServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Editor> editorRepository;
        private IDeletableEntityRepository<ApplicationUser> applicationUserRepository;
        private EditorService editorService;

        public EditorServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("EpsilonEditorDatabase")
                .Options;

            applicationDbContext = new ApplicationDbContext(contextOptions);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();

            editorRepository = new EfDeletableEntityRepository<Editor>(applicationDbContext);
            applicationUserRepository = new EfDeletableEntityRepository<ApplicationUser>(applicationDbContext);
            editorService = new EditorService(editorRepository);
        }

        [Fact]
        public async Task CreateAsyncWorksProperly()
        {
            var user = new ApplicationUser();

            await applicationUserRepository.AddAsync(user);
            await applicationUserRepository.SaveChangesAsync();

            await editorService.CreateAsync(user.Id);

            var dbModel = await editorRepository.All().FirstOrDefaultAsync();

            Assert.NotEmpty(editorRepository.All());
            Assert.NotNull(dbModel);
            Assert.Equal(user.Id, dbModel.ApplicationUser.Id);
        }

        [Fact]
        public async Task GetEditorIdAsyncWorksProperly()
        {
            var user = new ApplicationUser();

            await applicationUserRepository.AddAsync(user);
            await applicationUserRepository.SaveChangesAsync();

            var editor = new Editor()
            {
                Id = "1",
                ApplicationUser = user,
            };

            await editorRepository.AddAsync(editor);
            await editorRepository.SaveChangesAsync();

            var editorId = await editorService.GetEditorIdAsync(user.Id);

            Assert.Equal("1", editorId);
        }
    }
}
