using Epsilon.Services.Data.Contracts;
using Epsilon.Web.ViewModels.Computer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Epsilon.Web.Controllers
{
    public class ComputerController : BaseController
    {
        private readonly IComputerService computerService;

        public ComputerController(IComputerService _computerService)
        {
            computerService = _computerService;
        }

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
        public async Task<IActionResult> Create(ComputerCreateInputModel model)
        {
            // TODO: implement error handling (catch functionality)
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await computerService.CreateAsync(model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
