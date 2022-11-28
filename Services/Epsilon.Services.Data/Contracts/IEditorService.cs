using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface IEditorService
    {
        Task CreateAsync(string applicationUserId);

        Task<string> GetEditorIdAsync(string userId);
    }
}
