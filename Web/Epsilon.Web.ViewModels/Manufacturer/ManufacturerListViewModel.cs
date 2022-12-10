using System.Collections.Generic;

namespace Epsilon.Web.ViewModels.Manufacturer
{
    public class ManufacturerListViewModel
    {
        //// TODO: implement paging

        public IEnumerable<ManufacturerInListViewModel> Manufacturers { get; set; } = new List<ManufacturerInListViewModel>();
    }
}
