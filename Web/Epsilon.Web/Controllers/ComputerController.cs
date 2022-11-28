using Epsilon.Services.Data.Contracts;
using Epsilon.Web.Infrastructure.Extensions;
using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Computer;
using Epsilon.Web.ViewModels.Manufacturer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Epsilon.Web.Controllers
{
    public class ComputerController : BaseController
    {
        private readonly IComputerService computerService;
        private readonly ICategoryService categoriesService;
        private readonly IManufacturerService manufacturerService;

        public ComputerController(IComputerService _computerService, ICategoryService _categoriesService, IManufacturerService _manufacturerService)
        {
            computerService = _computerService;
            categoriesService = _categoriesService;
            manufacturerService = _manufacturerService;
        }

        public IActionResult All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ComputerCreateInputModel()
            {
                Categories = await categoriesService.GetAllAsync<CategoryDropdownViewModel>(),
                Manufacturers = await manufacturerService.GetAllAsync<ManufacturerDropdownViewModel>(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ComputerCreateInputModel model)
        {
            // TODO: implement error handling (catch functionality)
            if (!this.ModelState.IsValid)
            {
                model.Categories = await categoriesService.GetAllAsync<CategoryDropdownViewModel>();
                model.Manufacturers = await manufacturerService.GetAllAsync<ManufacturerDropdownViewModel>();

                return this.View(model);
            }

            try
            {
                await computerService.CreateAsync(model, User.Id());

                // return RedirectToAction(nameof(All));
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
