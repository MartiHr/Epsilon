using System.Diagnostics;
using System.Threading.Tasks;
using Epsilon.Services.Data.Contracts;
using Epsilon.Web.ViewModels;
using Epsilon.Web.ViewModels.Computer;
using Microsoft.AspNetCore.Mvc;

namespace Epsilon.Web.Controllers
{
    public class HomeController : BaseController
    {

        private readonly IComputerService computerService;

        public HomeController(IComputerService _computerService)
        {
            computerService = _computerService;
        }

        public async Task<IActionResult> Index()
        {
            const int itemsCount = 4;

            var model = new ComputersListViewModel()
            {
                Computers = await computerService.GetANumberOfAsync<ComputerInListViewModel>(itemsCount),
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
