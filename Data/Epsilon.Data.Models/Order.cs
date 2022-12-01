using System;
using System.ComponentModel.DataAnnotations;

using Epsilon.Data.Common.Models;

namespace Epsilon.Data.Models
{
    public class Order : BaseDeletableModel<string>
    {
        // TODO: add properties, add order status entity/enum
        public Order()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
