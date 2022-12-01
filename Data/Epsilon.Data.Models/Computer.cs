using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Epsilon.Data.Common.Models;

using static Epsilon.Data.Common.DataValidation.Computer;

namespace Epsilon.Data.Models
{
    public class Computer : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(ComputerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ComputerModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(ComputerDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Editor))]
        public string CreatorId { get; set; }

        public Editor Creator { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public ICollection<Part> Parts { get; set; } = new HashSet<Part>();
    }
}
