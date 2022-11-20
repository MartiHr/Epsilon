using System.Collections.Generic;

using Epsilon.Data.Common.Models;

namespace Epsilon.Data.Models
{
    public class Part : BaseDeletableModel<int>
    {
        // TODO: Add type entity
        public string Type { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public ICollection<Computer> Computers { get; set; } = new HashSet<Computer>();
    }
}
