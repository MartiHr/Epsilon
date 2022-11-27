using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Manufacturer;
using System.Collections.Generic;

namespace Epsilon.Web.ViewModels.Computer
{
    public class ComputerCreateInputModel
    {
        // TODO: add attributes and finish
        public string Name { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public IEnumerable<CategoryDropdownView> Categories { get; set; } = new List<CategoryDropdownView>();

        public IEnumerable<ManufacturerDropdownView> Manufacturers { get; set; } = new List<ManufacturerDropdownView>();

        // TODO: implement parts adding view
        // public ICollection<Part> Parts { get; set; } = new HashSet<Part>();
    }
}
