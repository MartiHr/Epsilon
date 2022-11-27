using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Epsilon.Data.Common.Models;

namespace Epsilon.Data.Models
{
    public class Computer : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Model { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public ICollection<Part> Parts { get; set; } = new HashSet<Part>();
    }
}
