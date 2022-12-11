using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Manufacturer;

using static Epsilon.Data.Common.DataValidation.Part;

using PartModel = Epsilon.Data.Models.Part;

namespace Epsilon.Web.ViewModels.Part
{
    public class PartEditInputModel : IMapFrom<PartModel>
    {
        public int Id { get; set; }

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
