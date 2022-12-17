using Epsilon.Web.ViewModels.Computer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Web.ViewModels.Order
{
    public class OrderListViewModel
    {
        public IEnumerable<OrderInListViewModel> Orders { get; set; } = new List<OrderInListViewModel>();
    }
}
