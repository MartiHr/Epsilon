﻿using Epsilon.Data.Models;
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

        public async Task<IActionResult> All(string customerId)
        {
            if (customerId == null)
            {
                customerId = await customerService.GetCustomerIdAsync(User.Id());
            }

            var model = new CartListViewModel()
            {
                Computers = await cartService.GetAllComputersOfCustomerCartAsync<ComputerInListViewModel>(customerId),
            };

            return View(model);
        }

        public async Task<IActionResult> AddComputer(int id)
        {
            var customerId = await customerService.GetCustomerIdAsync(User.Id());

            if (await customerService.HasCartAsync(customerId) == false)
            {
                await cartService.CreateCartAsync(customerId);
            }

            var cart = await cartService.GetCartByCustomerIdAsync(customerId);

            await cartService.AddComputerToCartAsync(id, cart.Id);

            return RedirectToAction(nameof(All), new { customerId = customerId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customerId = await customerService.GetCustomerIdAsync(User.Id());

            var cart = await cartService.GetCartByCustomerIdAsync(customerId);

            await cartService.RemoveComputerFromCartAsync(id, cart.Id);

            return RedirectToAction(nameof(All), new { customerId = customerId });
        }
    }
}
