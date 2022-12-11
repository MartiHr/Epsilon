using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Epsilon.Web.ViewModels.Manufacturer;

using static Epsilon.Data.Common.DataValidation.Part;

namespace Epsilon.Web.ViewModels.Part
{
    public class PartCreateInputModel
    {
        [Required]
        [StringLength(PartTypeMaxLength, MinimumLength = PartTypeMinLength)]
        public string Type { get; set; }

        [Required]
        [StringLength(PartModelMaxLength, MinimumLength = PartModelMinLength)]
        public string Model { get; set; }

        [Required]
        [StringLength(PartDescriptionMaxLength, MinimumLength = PartDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        public IEnumerable<ManufacturerDropdownViewModel> Manufacturers { get; set; } = new List<ManufacturerDropdownViewModel>();
    }
}
