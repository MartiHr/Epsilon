using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Epsilon.Common;
using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Manufacturer;
using Epsilon.Web.ViewModels.Part;
using Microsoft.AspNetCore.Http;

using static Epsilon.Data.Common.DataValidation.Computer;

namespace Epsilon.Web.ViewModels.Computer
{
    public class ComputerCreateInputModel
    {
        // TODO: add attributes and finish
        [StringLength(ComputerNameMaxLength, MinimumLength = ComputerNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ComputerModelMaxLength, MinimumLength = ComputerModelMinLength)]
        public string Model { get; set; }

        [Required]
        [Range(typeof(decimal), ComputerPriceMinValue, ComputerPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(ComputerDescriptionMaxLength, MinimumLength = ComputerDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; } = new List<CategoryDropdownViewModel>();

        [Required]
        public int ManufacturerId { get; set; }

        public IEnumerable<ManufacturerDropdownViewModel> Manufacturers { get; set; } = new List<ManufacturerDropdownViewModel>();

        [Required]
        public int CPUId { get; set; }

        public IEnumerable<PartDropdownViewModel> CPUs { get; set; } = new List<PartDropdownViewModel>();

        [Required]
        public int GPUId { get; set; }

        public IEnumerable<PartDropdownViewModel> GPUs { get; set; } = new List<PartDropdownViewModel>();

        [Required]
        public int StorageId { get; set; }

        public IEnumerable<PartDropdownViewModel> Storages { get; set; } = new List<PartDropdownViewModel>();

        [Required]
        [AllowedExtensionsForCollection(new string[] { GlobalConstants.PNGExtension, GlobalConstants.JPGExtension, GlobalConstants.JPEGExtension })]
        public ICollection<IFormFile> Images { get; set; }
    }
}
