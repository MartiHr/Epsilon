using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Epsilon.Data.Common.Models;

using static Epsilon.Data.Common.DataValidation.Manufacturer;

namespace Epsilon.Data.Models
{
    public class Manufacturer : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(ManufacturerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ManufacturerCountryMaxLength)]
        public string Country { get; set; }

        public ICollection<Computer> Computers { get; set; } = new HashSet<Computer>();

        public ICollection<Part> Parts { get; set; } = new HashSet<Part>();
    }
}
