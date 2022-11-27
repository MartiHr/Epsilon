using Epsilon.Web.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Epsilon.Web.Controllers
{
    public class CategoryController : BaseController
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
