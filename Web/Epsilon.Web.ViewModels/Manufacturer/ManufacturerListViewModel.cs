using System.Collections.Generic;

namespace Epsilon.Web.ViewModels.Manufacturer
{
    public class ManufacturerListViewModel
    {
        public IEnumerable<ManufacturerInListViewModel> Manufacturers { get; set; } = new List<ManufacturerInListViewModel>();
    }
}
