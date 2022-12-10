using System.ComponentModel.DataAnnotations;

using Epsilon.Services.Mapping;

using static Epsilon.Data.Common.DataValidation.Manufacturer;

using ManufacturerModel = Epsilon.Data.Models.Manufacturer;

namespace Epsilon.Web.ViewModels.Manufacturer
{
    public class ManufacturerEditInputModel : IMapFrom<ManufacturerModel>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ManufacturerNameMaxLength, MinimumLength = ManufacturerNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ManufacturerCountryMaxLength, MinimumLength = ManufacturerCountryMinLength)]
        public string Country { get; set; }
    }
}
