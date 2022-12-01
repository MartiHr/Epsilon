using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Epsilon.Data.Common.Models;

namespace Epsilon.Data.Models
{
    public class Editor : BaseDeletableModel<string>
    {
        public Editor()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Computer> CreatedComputers { get; set; } = new HashSet<Computer>();
    }
}
