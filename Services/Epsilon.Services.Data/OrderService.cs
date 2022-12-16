using System.Linq;
using System.Threading.Tasks;

using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Epsilon.Services.Data
{
    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> orderRepository;
        private readonly IComputerService computerService;

        public OrderService(IDeletableEntityRepository<Order> _orderRepository, IComputerService _computerService)
        {
            orderRepository = _orderRepository;
            computerService = _computerService;
        }

        public async Task AddComputerToOrderAsync(string orderId, int computerId)
        {
            var computer = await computerService.GetOneByIdAsync(computerId);

            var order = await orderRepository
                .All()
                .Where(o => o.Id == orderId)
                .Include(o => o.Computers)
                .FirstOrDefaultAsync();

            order.Computers.Add(computer);

            orderRepository.Update(order);
            await orderRepository.SaveChangesAsync();
        }

        public async Task<string> CreateAsync(string customerId, string address)
        {
            var order = new Order()
            {
                CustomerId = customerId,
                Address = address,
            };

            await orderRepository.AddAsync(order);
            await orderRepository.SaveChangesAsync();

            return order.Id;
        }
    }
}
