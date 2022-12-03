using System.Collections.Generic;

namespace Epsilon.Web.ViewModels.Computer
{
    public class ComputersListViewModel : PagingViewModel
    {
        public IEnumerable<ComputerInListViewModel> Computers { get; set; } = new List<ComputerInListViewModel>();
    }
}
