﻿using Epsilon.Data.Models;
using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Computer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface IComputerService
    {
        // TODO: implement
        Task CreateAsync(ComputerCreateInputModel model, string ownerId);
    }
}
