using System;
using System.Threading.Tasks;
using Epsilon.Services.Data.Contracts;
using Epsilon.Web.Infrastructure.Extensions;
using Epsilon.Web.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace Epsilon.Web.Areas.Administration.Controllers
{
    public class CategoryController : AdministrationController
    {
        private readonly ICategoryService categoriesService;

        public CategoryController(ICategoryService _categoriesService)
        {
            categoriesService = _categoriesService;
        }

        public async Task<IActionResult> All()
        {
            // TODO: implement pagination
            var model = new CategoryListViewModel()
            {
                Categories = await categoriesService.GetAllAsync<CategoryInListViewModel>(),
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var inputModel = new CategoryCreateInputModel();

            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return View(inputModel);
            }

            try
            {
                await categoriesService.CreateAsync(inputModel, User.Id());

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
                var inputModel = await categoriesService.GetOneByIdAsync<CategoryEditInputModel>(id);

                return View(inputModel);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(All));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return View(inputModel);
            }

            try
            {
                await categoriesService.EditByIdAsync(inputModel, User.Id());

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                // TODO: extract success, error and other messages into constants
                ModelState.AddModelError(string.Empty, "Something went wrong when editing");

                return View(inputModel);
            }
        }
    }
}
