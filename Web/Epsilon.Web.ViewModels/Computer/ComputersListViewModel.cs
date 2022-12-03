using System.Collections.Generic;

namespace Epsilon.Web.ViewModels.Computer
{
    public class ComputerListViewModel
    {
        public IEnumerable<ComputerInListViewModel> Computers { get; set; } = new List<ComputerInListViewModel>();

        public int PageNumber { get; set; }
    }
}
