using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface IEditorService
    {
        Task<string> GetEditorIdAsync(string userId);
    }
}
