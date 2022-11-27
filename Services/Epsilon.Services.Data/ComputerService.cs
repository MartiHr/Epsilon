﻿using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Epsilon.Web.ViewModels.Computer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Services.Data
{
    public class ComputerService : IComputerService
    {
        private readonly IDeletableEntityRepository<Computer> computerRepository;

        public ComputerService(IDeletableEntityRepository<Computer> _computerRepository)
        {
            computerRepository = _computerRepository;
        }

        public async Task CreateAsync(ComputerCreateInputModel model, string ownerId)
        {
            var computer = new Computer()
            {

            };
        }
    }
}
