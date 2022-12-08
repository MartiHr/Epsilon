using System.Threading.Tasks;

using Epsilon.Web.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace Epsilon.Web.Areas.Administration.Controllers
{
    public class CategoryController : AdministrationController
    {
        public IActionResult All()
        {
            return View();
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
