using Epsilon.Common;
using Epsilon.Services.Data;
using Epsilon.Services.Data.Contracts;
using Epsilon.Web.Infrastructure.Extensions;
using Epsilon.Web.ViewModels.Cart;
using Epsilon.Web.ViewModels.Computer;
using Epsilon.Web.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Epsilon.Web.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly ICustomerService customerService;
        private readonly ICartService cartService;

        public OrderController(IOrderService _orderService,
            ICustomerService _customerService,
            ICartService _cartService)
        {
            orderService = _orderService;
            customerService = _customerService;
            cartService = _cartService;
        }

        public async Task<IActionResult> All()
        {
            var customerId = await customerService.GetCustomerIdAsync(User.Id());

            var orders = await orderService.GetAllAsync<OrderInListViewModel>(customerId);

            foreach (var order in orders)
            {
                order.Computers = await orderService.GetAllComputersOfOrderAsync<ComputerInListViewModel>(customerId, order.Id);
            }

            var model = new OrderListViewModel()
            {
                Orders = orders,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CartListViewModel model)
        {
            var customerId = await customerService.GetCustomerIdAsync(User.Id());

            var orderId = await orderService.CreateAsync(customerId, model.Address);

            var computers = await cartService.GetAllComputersOfCustomerCartAsync<ComputerInListViewModel>(customerId);

            foreach (var computer in computers)
            {
                await orderService.AddComputerToOrderAsync(orderId, computer.Id);
            }

            await cartService.EmptyAsync(customerId);

            return RedirectToAction(nameof(All));
        }
    }
}
