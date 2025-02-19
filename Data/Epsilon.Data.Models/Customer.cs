﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Epsilon.Data.Common.Models;

namespace Epsilon.Data.Models
{
    public class Customer : BaseDeletableModel<string>
    {
        public Customer()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        public Cart Cart { get; set; }
    }
}
