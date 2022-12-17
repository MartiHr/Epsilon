using Epsilon.Common;
using Epsilon.Services.Data.Contracts;
using Epsilon.Web.Infrastructure.Extensions;
using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Manufacturer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Epsilon.Web.Areas.Administration.Controllers
{
    public class ManufacturerController : AdministrationController
    {
        private readonly IManufacturerService manufacturerService;

        public ManufacturerController(IManufacturerService _manufacturerService)
        {
            manufacturerService = _manufacturerService;
        }

        public async Task<IActionResult> All()
        {
            var model = new ManufacturerListViewModel()
            {
                Manufacturers = await manufacturerService.GetAllWithDeletedAsync<ManufacturerInListViewModel>(),
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var inputModel = new ManufacturerCreateInputModel();

            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ManufacturerCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return View(inputModel);
            }

            try
            {
                await manufacturerService.CreateAsync(inputModel, User.Id());

                TempData[GlobalConstants.SuccessMessage] = GlobalConstants.SuccessfullyAddedMessage;

                return RedirectToAction(nameof(All));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View(inputModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var inputModel = await manufacturerService.GetOneByIdAsync<ManufacturerEditInputModel>(id);

                return View(inputModel);
            }
            catch (Exception e)
            {
                TempData[GlobalConstants.WarningMessage] = GlobalConstants.UnexpectedError;

                return RedirectToAction(nameof(All));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ManufacturerEditInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return View(inputModel);
            }

            try
            {
                await manufacturerService.EditByIdAsync(inputModel, User.Id());

                TempData[GlobalConstants.SuccessMessage] = GlobalConstants.SuccessfullyChangedMessage;

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                TempData[GlobalConstants.WarningMessage] = GlobalConstants.UnexpectedError;
                ModelState.AddModelError(string.Empty, "Something went wrong while editing");

                return View(inputModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await manufacturerService.DeleteByIdAsync(id);

                TempData[GlobalConstants.SuccessMessage] = GlobalConstants.SuccessfullyRemovedMessage;

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                TempData[GlobalConstants.WarningMessage] = GlobalConstants.UnexpectedError;
                ModelState.AddModelError(string.Empty, "Something went wrong while deleting");

                return RedirectToAction(nameof(All));
            }
        }
    }
}
