using Epsilon.Data.Models;
using Epsilon.Services.Data;
using Epsilon.Services.Data.Contracts;
using Epsilon.Web.Infrastructure.Extensions;
using Epsilon.Web.ViewModels.Cart;
using Epsilon.Web.ViewModels.Computer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Epsilon.Web.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private readonly ICartService cartService;
        private readonly ICustomerService customerService;

        public CartController(ICartService _cartService, ICustomerService _customerService)
        {
            cartService = _cartService;
            customerService = _customerService;
        }

        public async Task<IActionResult> CartAll(string customerId)
        {
            var model = new CartListViewModel()
            {
                Computers = await cartService.GetAllComputersOfCustomerAsync<ComputerInListViewModel>(customerId),
            };

            return View(model);
        }

        public async Task<IActionResult> AddComputer(int computerId)
        {
            var customerId = await customerService.GetCustomerIdAsync(User.Id());

            if (await customerService.HasCartAsync(customerId))
            {
                await cartService.CreateCartAsync(customerId);
            }

            var cart = await cartService.GetCartByCustomerIdAsync(customerId);

            await cartService.AddComputerToCartAsync(computerId, cart.Id);

            return RedirectToAction(nameof(CartAll), new { customerId = customerId });
        }
    }
}
