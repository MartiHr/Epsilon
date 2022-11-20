using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Epsilon.Data.Common.Models;

namespace Epsilon.Data.Models
{
    public class Computer : BaseDeletableModel<int>
    {
        public string Type { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public string Description { get; set; } = null!;

        public Manufacturer Manufacturer { get; set; }

        public ICollection<Part> Parts { get; set; } = new HashSet<Part>();
    }
}
