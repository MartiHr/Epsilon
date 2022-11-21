using System.Collections;
using System.Collections.Generic;

using Epsilon.Data.Common.Models;

namespace Epsilon.Data.Models
{
    public class Category : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public ICollection<Computer> Computers { get; set; } = new HashSet<Computer>();
    }
}
