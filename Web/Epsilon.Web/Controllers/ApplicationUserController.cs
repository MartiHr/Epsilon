﻿using System.Threading.Tasks;

using Epsilon.Common;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Epsilon.Web.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Epsilon.Web.Controllers
{
    [Authorize]
    public class ApplicationUserController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ICustomerService customerService;
        private readonly IEditorService editorService;

        public ApplicationUserController(
                UserManager<ApplicationUser> _userManager,
                SignInManager<ApplicationUser> _signInManager,
                ICustomerService _customerService,
                IEditorService _editorService)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            customerService = _customerService;
            editorService = _editorService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await customerService.CreateAsync(user.Id);

                TempData[GlobalConstants.SuccessMessage] = "Successfully registered!";

                return RedirectToAction(nameof(Login));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();

            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    TempData[GlobalConstants.SuccessMessage] = "Successfully logged in!";

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            TempData[GlobalConstants.WarningMessage] = "Logged out!";

            return RedirectToAction("Index", "Home");
        }
    }
}
