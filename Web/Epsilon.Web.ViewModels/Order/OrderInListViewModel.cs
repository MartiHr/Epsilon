using System;
using System.Collections.Generic;
using System.Linq;

using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Computer;

using OrderModel = Epsilon.Data.Models.Order;

namespace Epsilon.Web.ViewModels.Order
{
    public class OrderInListViewModel : IMapFrom<OrderModel>
    {
        public string Id { get; set; }

        public string Address { get; set; }

        public IEnumerable<ComputerInListViewModel> Computers { get; set; } = new List<ComputerInListViewModel>();

        public DateTime CreatedOn { get; set; }

        public string TotalPrice => Computers.Select(c => c.Price).Sum().ToString("f2");
    }
}
