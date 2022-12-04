using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Epsilon.Services.Data.Contracts;
using Epsilon.Web.Infrastructure.Extensions;
using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Computer;
using Epsilon.Web.ViewModels.Manufacturer;
using Epsilon.Web.ViewModels.Part;
using Microsoft.AspNetCore.Mvc;

namespace Epsilon.Web.Controllers
{
    public class ComputerController : BaseController
    {
        private readonly IComputerService computerService;
        private readonly ICategoryService categoriesService;
        private readonly IManufacturerService manufacturerService;
        private readonly IEditorService editorService;
        private readonly IPartService partService;

        public ComputerController
            (IComputerService _computerService,
            ICategoryService _categoriesService,
            IManufacturerService _manufacturerService,
            IEditorService _editorService,
            IPartService _partService)
        {
            computerService = _computerService;
            categoriesService = _categoriesService;
            manufacturerService = _manufacturerService;
            editorService = _editorService;
            partService = _partService;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            const int ItemsPerPage = 8;
            var model = new ComputersListViewModel()
            {
                PageNumber = id,
                ComputersCount = computerService.GetCount(),
                ItemsPerPage = ItemsPerPage,
                Computers = await computerService.GetAllAsync<ComputerInListViewModel>(id, ItemsPerPage),
            };

            return View(model);
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

                return this.View(inputModel);
            }

            try
            {
                var creatorId = await editorService.GetEditorIdAsync(User.Id());

                await computerService.CreateAsync(inputModel, creatorId);

                // return RedirectToAction(nameof(All));
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                // TODO: add toastr
                ModelState.AddModelError(string.Empty, e.Message);

                await DecorateComputerCreateInputModel(inputModel);

                return View(inputModel);
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
    }
}
