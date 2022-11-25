﻿using System.Diagnostics;

using Epsilon.Web.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace Epsilon.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

            }

            return this.View();
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
