using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Epsilon.Data.Common.Models;

namespace Epsilon.Data.Models
{
    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public string Address { get; set; }

        public ICollection<Computer> Computers { get; set; } = new HashSet<Computer>();
    }
}
