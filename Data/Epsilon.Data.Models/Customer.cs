using System;
using System.Collections.Generic;

using Epsilon.Data.Common.Models;

namespace Epsilon.Data.Models
{
    public class Customer : BaseDeletableModel<string>
    {
        // TODO: Add customer specific properties
        public Customer()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
