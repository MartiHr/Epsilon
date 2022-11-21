using Epsilon.Data.Common.Models;

namespace Epsilon.Data.Models
{
    public class Order : BaseDeletableModel<int>
    {
        // TODO: add order status entity/enum

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
