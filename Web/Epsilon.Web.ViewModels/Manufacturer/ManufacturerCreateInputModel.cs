using System.ComponentModel.DataAnnotations;

using static Epsilon.Data.Common.DataValidation.Manufacturer;

namespace Epsilon.Web.ViewModels.Manufacturer
{
    public class ManufacturerCreateInputModel
    {
        [Required]
        [StringLength(ManufacturerNameMaxLength, MinimumLength = ManufacturerNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ManufacturerCountryMaxLength, MinimumLength = ManufacturerCountryMinLength)]
        public string Country { get; set; }
    }
}
