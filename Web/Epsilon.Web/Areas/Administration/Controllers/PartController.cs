using Epsilon.Services.Data.Contracts;
using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Part;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Epsilon.Web.Areas.Administration.Controllers
{
    public class PartController : AdministrationController
    {

        private readonly IPartService partService;

        public PartController(IPartService _partService)
        {
            partService = _partService;
        }

        public async Task<IActionResult> All()
        {
            // TODO: implement pagination
            var model = new PartListViewModel()
            {
                Parts = await partService.GetAllWithDeletedAsync<PartInListViewModel>(),
            };

            return View(model);
        }
    }
}
