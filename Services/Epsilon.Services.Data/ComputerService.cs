﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Computer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Epsilon.Services.Data
{
    public class ComputerService : IComputerService
    {
        private readonly IDeletableEntityRepository<Computer> computerRepository;
        private readonly IDeletableEntityRepository<Part> partRepository;

        public ComputerService(IDeletableEntityRepository<Computer> _computerRepository,
            IDeletableEntityRepository<Part> _partRepository)
        {
            computerRepository = _computerRepository;
            partRepository = _partRepository;
        }

        public async Task CreateAsync(ComputerCreateInputModel model, string creatorId)
        {
            var parts = await partRepository
                .AllAsNoTracking()
                .Where(p => p.Id == model.GPUId || p.Id == model.CPUId || p.Id == model.StorageId)
                .ToListAsync();

            // parts.Add(new Part() { Id = model.CPUId, });
            // parts.Add(new Part() { Id = model.GPUId });
            // parts.Add(new Part() { Id = model.StorageId });

            var computer = new Computer()
            {
                Name = model.Name,
                Model = model.Model,
                Price = model.Price,
                Description = model.Description,
                CategoryId = model.CategoryId,
                ManufacturerId = model.ManufacturerId,
                CreatorId = creatorId,
            };

            await computerRepository.AddAsync(computer);
            await computerRepository.SaveChangesAsync();

            computer.Parts = parts;

            computerRepository.Update(computer);
            await computerRepository.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync<T>(int page, int itemsPerPage)
        {
            var items = await computerRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToListAsync();

            return items;
        }

        public int GetCount()
        {
            return computerRepository
                .AllAsNoTracking()
                .Count();
        }
    }
}
