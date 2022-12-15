using System.Collections.Generic;
using System.Linq;

using Epsilon.Web.ViewModels.Computer;

namespace Epsilon.Web.ViewModels.Cart
{
    public class CartListViewModel
    {
        public IEnumerable<ComputerInListViewModel> Computers { get; set; } = new List<ComputerInListViewModel>();

        public string TotalPrice => Computers.Select(c => c.Price).Sum().ToString("f2");
    }
}
