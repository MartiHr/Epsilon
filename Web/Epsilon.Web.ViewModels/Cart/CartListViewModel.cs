using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Epsilon.Web.ViewModels.Computer;

using static Epsilon.Data.Common.DataValidation.Address;

namespace Epsilon.Web.ViewModels.Cart
{
    public class CartListViewModel
    {
        public IEnumerable<ComputerInListViewModel> Computers { get; set; } = new List<ComputerInListViewModel>();

        [Required]
        [StringLength(AddressTypeMaxLength, MinimumLength = AddressTypeMinLength)]
        public string Address { get; set; }

        public string TotalPrice => Computers.Select(c => c.Price).Sum().ToString("f2");
    }
}
