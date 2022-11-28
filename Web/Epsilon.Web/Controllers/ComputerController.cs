using Epsilon.Services.Data.Contracts;
using Epsilon.Web.Infrastructure.Extensions;
using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Computer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Epsilon.Web.Controllers
{
    public class ComputerController : BaseController
    {
        private readonly IComputerService computerService;

        private readonly ICategoryService categoriesService;

        public ComputerController(IComputerService _computerService, ICategoryService _categoriesService)
        {
            computerService = _computerService;
            categoriesService = _categoriesService;
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
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ComputerCreateInputModel model)
        {
            // TODO: implement error handling (catch functionality)
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await computerService.CreateAsync(model, User.Id());
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
