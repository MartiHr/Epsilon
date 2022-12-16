using Epsilon.Common;
using Epsilon.Services.Data.Contracts;
using Epsilon.Web.Infrastructure.Extensions;
using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Computer;
using Epsilon.Web.ViewModels.Manufacturer;
using Epsilon.Web.ViewModels.Part;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Epsilon.Web.Areas.Administration.Controllers
{
    public class ComputerController : AdministrationController
    {
        private readonly IComputerService computerService;
        private readonly ICategoryService categoriesService;
        private readonly IManufacturerService manufacturerService;
        private readonly IEditorService editorService;
        private readonly IPartService partService;
        private readonly IWebHostEnvironment hostEnvironment;

        public ComputerController
            (IComputerService _computerService,
            ICategoryService _categoriesService,
            IManufacturerService _manufacturerService,
            IEditorService _editorService,
            IPartService _partService,
            IWebHostEnvironment _hostEnvironment)
        {
            computerService = _computerService;
            categoriesService = _categoriesService;
            manufacturerService = _manufacturerService;
            editorService = _editorService;
            partService = _partService;
            hostEnvironment = _hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ComputerCreateInputModel();
            await DecorateComputerCreateInputModel(model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ComputerCreateInputModel inputModel)
        {
            // TODO: implement error handling (catch functionality)
            if (!this.ModelState.IsValid)
            {
                await DecorateComputerCreateInputModel(inputModel);

                return View(inputModel);
            }

            try
            {
                var creatorId = await editorService.GetEditorIdAsync(User.Id());

                await computerService.CreateAsync(inputModel, creatorId, $"{hostEnvironment.WebRootPath}/images");

                return RedirectToAction("All", "Computer", new { Area = string.Empty });
            }
            catch (Exception e)
            {
                TempData[GlobalConstants.ErrorMessage] = e.Message;
                ModelState.AddModelError(string.Empty, e.Message);

                await DecorateComputerCreateInputModel(inputModel);

                return View(inputModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var inputModel = await computerService.GetByIdAsync<ComputerEditInputModel>(id);

                await DecorateComputerEditInputModel(inputModel);

                return View(inputModel);
            }
            catch (Exception e)
            {
                TempData[GlobalConstants.ErrorMessage] = e.Message;
                return RedirectToAction("All", "Computer", new { Area = string.Empty });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ComputerEditInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                await DecorateComputerEditInputModel(inputModel);

                return View(inputModel);
            }

            try
            {
                var creatorId = await editorService.GetEditorIdAsync(User.Id());

                await computerService.EditByIdAsync(inputModel, creatorId, $"{hostEnvironment.WebRootPath}/images");

                return RedirectToAction("Details", "Computer", new { id = inputModel.Id, Area = string.Empty });
            }
            catch (Exception e)
            {
                TempData[GlobalConstants.ErrorMessage] = e.Message;
                ModelState.AddModelError(string.Empty, e.Message);

                await DecorateComputerEditInputModel(inputModel);

                return View(inputModel);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await computerService.DeleteAsync(id);

                TempData[GlobalConstants.SuccessMessage] = "Successfully deleted";

                return RedirectToAction("All", "Computer", new { Area = string.Empty });
            }
            catch (Exception e)
            {
                TempData[GlobalConstants.ErrorMessage] = e.Message;
                return RedirectToAction("All", "Computer", new { Area = string.Empty });
            }
        }

        private async Task DecorateComputerCreateInputModel(ComputerCreateInputModel model)
        {
            // TODO: extract logic elsewhere
            model.Categories = await categoriesService.GetAllAsync<CategoryDropdownViewModel>();
            model.Manufacturers = await manufacturerService.GetAllAsync<ManufacturerDropdownViewModel>();
            model.GPUs = await partService.GetAllOfTypeAsync<PartDropdownViewModel>("GPU");
            model.CPUs = await partService.GetAllOfTypeAsync<PartDropdownViewModel>("CPU");
            model.Storages = await partService.GetAllOfTypeAsync<PartDropdownViewModel>("Storage");
        }

        private async Task DecorateComputerEditInputModel(ComputerEditInputModel model)
        {
            // TODO: extract logic elsewhere
            model.Categories = await categoriesService.GetAllAsync<CategoryDropdownViewModel>();
            model.Manufacturers = await manufacturerService.GetAllAsync<ManufacturerDropdownViewModel>();
            model.GPUs = await partService.GetAllOfTypeAsync<PartDropdownViewModel>("GPU");
            model.CPUs = await partService.GetAllOfTypeAsync<PartDropdownViewModel>("CPU");
            model.Storages = await partService.GetAllOfTypeAsync<PartDropdownViewModel>("Storage");
        }
    }
}
