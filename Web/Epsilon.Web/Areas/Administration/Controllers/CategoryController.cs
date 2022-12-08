using System.Threading.Tasks;
using Epsilon.Services.Data.Contracts;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateInputModel model)
        {
            return View();
        }
    }
}
