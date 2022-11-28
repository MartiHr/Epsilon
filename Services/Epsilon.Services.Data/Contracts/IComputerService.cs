using Epsilon.Web.ViewModels.Computer;
using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface IComputerService
    {
        // TODO: implement
        Task CreateAsync(ComputerCreateInputModel model, string ownerId);
    }
}
