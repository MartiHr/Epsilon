using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Epsilon.Services.Data
{
    public class EditorService : IEditorService
    {
        private readonly IDeletableEntityRepository<Editor> editorRepository;

        public EditorService(IDeletableEntityRepository<Editor> _editorRepository)
        {
            editorRepository = _editorRepository;
        }

        public async Task CreateAsync(string applicationUserId)
        {
            var editor = new Editor()
            {
                ApplicationUserId = applicationUserId,
            };

            await editorRepository.AddAsync(editor);
            await editorRepository.SaveChangesAsync();
        }

        public async Task<string> GetEditorIdAsync(string userId)
        {
            return await editorRepository
             .AllAsNoTracking()
             .Where(e => e.ApplicationUserId == userId)
             .Select(e => e.Id)
             .FirstOrDefaultAsync();
        }
    }
}
