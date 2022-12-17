using System;
using System.Threading.Tasks;
using Epsilon.Common;
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
            var model = new CategoryListViewModel()
            {
                Categories = await categoriesService.GetAllWithDeletedAsync<CategoryInListViewModel>(),
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
                await categoriesService.DeleteByIdAsync(id);

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
