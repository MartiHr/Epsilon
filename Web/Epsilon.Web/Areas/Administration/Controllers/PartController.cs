using Epsilon.Common;
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
            try
            {
                var model = new PartListViewModel()
                {
                    Parts = await partService.GetAllWithDeletedAsync<PartInListViewModel>(),
                };

                return View(model);
            }
            catch (Exception)
            {
                TempData[GlobalConstants.WarningMessage] = GlobalConstants.UnexpectedError;

                return RedirectToAction("All", "Computer");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var inputModel = new PartCreateInputModel();

                inputModel.Manufacturers = await manufacturerService.GetAllAsync<ManufacturerDropdownViewModel>();

                return View(inputModel);
            }
            catch (Exception)
            {
                TempData[GlobalConstants.WarningMessage] = GlobalConstants.UnexpectedError;

                return RedirectToAction("All", "Computer");
            }
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

                TempData[GlobalConstants.SuccessMessage] = GlobalConstants.SuccessfullyAddedMessage;

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
                TempData[GlobalConstants.WarningMessage] = GlobalConstants.UnexpectedError;

                return RedirectToAction(nameof(All));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PartEditInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return View(inputModel);
            }

            try
            {
                await partService.EditByIdAsync(inputModel, User.Id());

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
                await partService.DeleteByIdAsync(id);

                TempData[GlobalConstants.SuccessMessage] = GlobalConstants.SuccessfullyRemovedMessage;

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong while deleting");
                TempData[GlobalConstants.WarningMessage] = GlobalConstants.UnexpectedError;

                return RedirectToAction(nameof(All));
            }
        }
    }
}
