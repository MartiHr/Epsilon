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

        public IActionResult All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await GetComputerCreateInputModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ComputerCreateInputModel inputModel)
        {
            // TODO: implement error handling (catch functionality)
            if (!this.ModelState.IsValid)
            {
                /*inputModel.Categories = await categoriesService.GetAllAsync<CategoryDropdownViewModel>();
                inputModel.Manufacturers = await manufacturerService.GetAllAsync<ManufacturerDropdownViewModel>();

                var partsDropdownModels = await partService.GetAllAsync<PartDropdownViewModel>();

                var partsGroups = new List<PartGroupViewModel>()
                {
                    new PartGroupViewModel()
                    {
                        PartType = "GPU",
                        Parts = await partService.GetAllOfTypeAsync<PartDropdownViewModel>("GPU"),
                    },
                    new PartGroupViewModel()
                    {
                        PartType = "CPU",
                        Parts = await partService.GetAllOfTypeAsync<PartDropdownViewModel>("CPU"),
                    },
                    new PartGroupViewModel()
                    {
                        PartType = "Storage",
                        Parts = await partService.GetAllOfTypeAsync<PartDropdownViewModel>("Storage"),
                    },
                };

                inputModel.PartsGroups = new List<PartGroupViewModel>();*/

                inputModel = await GetComputerCreateInputModel();

                return this.View(inputModel);
            }

            try
            {
                var creatorId = await editorService.GetEditorIdAsync(User.Id());

                await computerService.CreateAsync(inputModel, creatorId);

                // return RedirectToAction(nameof(All));
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                // TODO: add toastr
                ModelState.AddModelError(string.Empty, "Unexpected error");

                /*inputModel.Categories = await categoriesService.GetAllAsync<CategoryDropdownViewModel>();
                inputModel.Manufacturers = await manufacturerService.GetAllAsync<ManufacturerDropdownViewModel>();*/
                inputModel = await GetComputerCreateInputModel();

                return View(inputModel);
            }
        }

        private async Task<ComputerCreateInputModel> GetComputerCreateInputModel()
        {
            return new ComputerCreateInputModel()
            {
                Categories = await categoriesService.GetAllAsync<CategoryDropdownViewModel>(),
                Manufacturers = await manufacturerService.GetAllAsync<ManufacturerDropdownViewModel>(),
                PartsGroups = new List<PartGroupViewModel>()
                {
                    new PartGroupViewModel()
                    {
                        PartType = "GPU",
                        Parts = await partService.GetAllOfTypeAsync<PartDropdownViewModel>("GPU"),
                    },
                    new PartGroupViewModel()
                    {
                        PartType = "CPU",
                        Parts = await partService.GetAllOfTypeAsync<PartDropdownViewModel>("CPU"),
                    },
                    new PartGroupViewModel()
                    {
                        PartType = "Storage",
                        Parts = await partService.GetAllOfTypeAsync<PartDropdownViewModel>("Storage"),
                    },
                },
            };
        }
    }
}
