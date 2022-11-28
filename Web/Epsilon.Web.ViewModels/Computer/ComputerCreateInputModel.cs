using System.Collections.Generic;

using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Manufacturer;

namespace Epsilon.Web.ViewModels.Computer
{
    public class ComputerCreateInputModel
    {
        // TODO: add attributes and finish
        public string Name { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; } = new List<CategoryDropdownViewModel>();

        public int ManufacturerId { get; set; }

        public IEnumerable<ManufacturerDropdownViewModel> Manufacturers { get; set; } = new List<ManufacturerDropdownViewModel>();

        // TODO: implement parts adding view
        // public ICollection<Part> Parts { get; set; } = new HashSet<Part>();
    }
}
