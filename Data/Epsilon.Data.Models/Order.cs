using Epsilon.Data.Common.Models;
using System;

namespace Epsilon.Data.Models
{
    public class Order : BaseDeletableModel<string>
    {
        // TODO: add order status entity/enum
        public Order()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
