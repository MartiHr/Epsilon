using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Epsilon.Common;
using Epsilon.Services.Data.Contracts;
using Epsilon.Web.Infrastructure.Extensions;
using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Computer;
using Epsilon.Web.ViewModels.Manufacturer;
using Epsilon.Web.ViewModels.Part;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Epsilon.Web.Controllers
{
    [Authorize]
    public class ComputerController : BaseController
    {
        private readonly IComputerService computerService;
        private readonly ICategoryService categoriesService;
        private readonly IManufacturerService manufacturerService;
        private readonly IEditorService editorService;
        private readonly IPartService partService;
        private readonly IWebHostEnvironment hostEnvironment;

        public ComputerController
            (IComputerService _computerService,
            ICategoryService _categoriesService,
            IManufacturerService _manufacturerService,
            IEditorService _editorService,
            IPartService _partService,
            IWebHostEnvironment _hostEnvironment)
        {
            computerService = _computerService;
            categoriesService = _categoriesService;
            manufacturerService = _manufacturerService;
            editorService = _editorService;
            partService = _partService;
            hostEnvironment = _hostEnvironment;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            const int ItemsPerPage = 4;
            var model = new ComputersListViewModel()
            {
                PageNumber = id,
                ComputersCount = computerService.GetCount(),
                ItemsPerPage = ItemsPerPage,
                Computers = await computerService.GetAllAsync<ComputerInListViewModel>(id, ItemsPerPage),
            };

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var computer = await computerService.GetByIdAsync<ComputerDetailsViewModel>(id);

                return View(computer);
            }
            catch (Exception e)
            {
                TempData[GlobalConstants.ErrorMessage] = e.Message;
                return RedirectToAction(nameof(All));
            }
        }
    }
}
