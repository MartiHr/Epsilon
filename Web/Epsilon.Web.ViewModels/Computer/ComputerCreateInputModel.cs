using System.Collections.Generic;

using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Manufacturer;
using Epsilon.Web.ViewModels.Part;

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

        public int CPUId { get; set; }

        public IEnumerable<PartDropdownViewModel> CPUs { get; set; } = new List<PartDropdownViewModel>();

        public int GPUId { get; set; }

        public IEnumerable<PartDropdownViewModel> GPUs { get; set; } = new List<PartDropdownViewModel>();

        public int StorageId { get; set; }

        public IEnumerable<PartDropdownViewModel> Storages { get; set; } = new List<PartDropdownViewModel>();

        // TODO: implement parts adding view
        // public ICollection<Part> Parts { get; set; } = new HashSet<Part>();

        //public List<int> PartsGroupsIds { get; set; }

        //public IEnumerable<PartGroupViewModel> PartsGroups { get; set; } = new List<PartGroupViewModel>();
    }
}
