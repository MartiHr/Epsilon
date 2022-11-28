using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;

namespace Epsilon.Services.Data
{
    public class EditorService : IEditorService
    {
        private readonly IDeletableEntityRepository<Editor> editorRepository;

        public EditorService(IDeletableEntityRepository<Editor> _editorRepository)
        {
            editorRepository = _editorRepository;
        }


    }
}
