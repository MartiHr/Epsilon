using Epsilon.Data.Common.Models;
using System;
using System.Collections.Generic;

namespace Epsilon.Data.Models
{
    public class Editor : BaseDeletableModel<string>
    {
        public Editor()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Computer> CreatedComputers { get; set; } = new HashSet<Computer>();
    }
}
