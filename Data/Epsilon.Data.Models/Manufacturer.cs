using System.Collections.Generic;

using Epsilon.Data.Common.Models;

namespace Epsilon.Data.Models
{
    public class Manufacturer : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public ICollection<Computer> Computers { get; set; } = new HashSet<Computer>();

        public ICollection<Part> Parts { get; set; } = new HashSet<Part>();
    }
}
