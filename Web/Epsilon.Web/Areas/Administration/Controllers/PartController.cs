using Epsilon.Services.Data;
using Epsilon.Services.Data.Contracts;
using Epsilon.Web.Infrastructure.Extensions;
using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Manufacturer;
using Epsilon.Web.ViewModels.Part;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Epsilon.Web.Areas.Administration.Controllers
{
    public class PartController : AdministrationController
    {

        private readonly IPartService partService;
        private readonly IManufacturerService manufacturerService;

        public PartController(IPartService _partService, IManufacturerService _manufacturerService)
        {
            partService = _partService;
            manufacturerService = _manufacturerService;
        }

        public async Task<IActionResult> All()
        {
            // TODO: implement pagination
            var model = new PartListViewModel()
            {
                Parts = await partService.GetAllWithDeletedAsync<PartInListViewModel>(),
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var inputModel = new PartCreateInputModel();

            inputModel.Manufacturers = await manufacturerService.GetAllAsync<ManufacturerDropdownViewModel>();

            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.Manufacturers = await manufacturerService.GetAllAsync<ManufacturerDropdownViewModel>();

                return View(inputModel);
            }

            try
            {
                await partService.CreateAsync(inputModel, User.Id());

                return RedirectToAction(nameof(All));
            }
            catch (Exception e)
            {
                inputModel.Manufacturers = await manufacturerService.GetAllAsync<ManufacturerDropdownViewModel>();

                ModelState.AddModelError(string.Empty, e.Message);

                return View(inputModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var inputModel = await partService.GetOneByIdAsync<PartEditInputModel>(id);
                inputModel.Manufacturers = await manufacturerService.GetAllAsync<ManufacturerDropdownViewModel>();

                return View(inputModel);
            }
            catch (Exception e)
            {
                inputModel.Manufacturers = await manufacturerService.GetAllAsync<ManufacturerDropdownViewModel>();

                return RedirectToAction(nameof(All));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await manufacturerService.DeleteByIdAsync(id);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                // TODO: extract success, error and other messages into constants
                ModelState.AddModelError(string.Empty, "Something went wrong while editing");

                return RedirectToAction(nameof(All));
            }
        }
    }
}
