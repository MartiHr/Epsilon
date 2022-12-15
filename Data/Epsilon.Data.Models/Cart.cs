using Epsilon.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Epsilon.Data.Models
{

    public class Cart : BaseDeletableModel<string>
    {
        public Cart()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string CustomerId { get; set; }

        public Customer Customer { get; set; }

        public ICollection<Computer> Computers { get; set; } = new HashSet<Computer>();
    }
}
