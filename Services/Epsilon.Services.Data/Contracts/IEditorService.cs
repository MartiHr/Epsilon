using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface IEditorService
    {
        Task GetEditorIdAsync(string userId);
    }
}
