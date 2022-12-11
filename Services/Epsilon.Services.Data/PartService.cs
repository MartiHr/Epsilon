﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Manufacturer;
using Epsilon.Web.ViewModels.Part;
using Microsoft.EntityFrameworkCore;

namespace Epsilon.Services.Data
{
    public class PartService : IPartService
    {
        private readonly IDeletableEntityRepository<Part> partRepository;

        public PartService(IDeletableEntityRepository<Part> _partRepository)
        {
            partRepository = _partRepository;
        }

        public async Task AssignComputerToExistingPartAsync(Computer computer, int partId)
        {
            var part = await partRepository
                .All()
                .Where(p => p.Id == partId)
                .FirstOrDefaultAsync();

            part.Computers.Add(computer);
            await partRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(PartCreateInputModel inputModel, string creatorId)
        {
            var part = new Part()
            {
                Type = inputModel.Type,
                Model = inputModel.Model,
                Description = inputModel.Description,
                ManufacturerId = inputModel.ManufacturerId,
            };

            await partRepository.AddAsync(part);
            await partRepository.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync<T>()
        {
            return await partRepository
                .AllAsNoTracking()
                .OrderBy(p => p.Model)
                .To<T>()
                .ToListAsync();
        }

        public async Task<List<T>> GetAllOfTypeAsync<T>(string type)
        {
            return await partRepository
                .AllAsNoTracking()
                .Where(p => p.Type == type)
                .OrderBy(p => p.Model)
                .To<T>()
                .ToListAsync();
        }

        public async Task<List<T>> GetAllWithDeletedAsync<T>()
        {
            return await partRepository
                .AllAsNoTrackingWithDeleted()
                .OrderBy(p => p.Type)
                .ThenByDescending(p => p.CreatedOn)
                .To<T>()
                .ToListAsync();
        }
    }
}
