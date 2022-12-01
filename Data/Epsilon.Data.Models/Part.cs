using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Epsilon.Data.Common.Models;

using static Epsilon.Data.Common.DataValidation.Part;

namespace Epsilon.Data.Models
{
    public class Part : BaseDeletableModel<int>
    {
        // TODO: Add Type entity/enum
        [Required]
        [MaxLength(PartTypeMaxLength)]
        public string Type { get; set; }

        [Required]
        [MaxLength(PartModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(PartDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public ICollection<Computer> Computers { get; set; } = new HashSet<Computer>();
    }
}
