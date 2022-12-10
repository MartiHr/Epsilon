using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Category;
using Epsilon.Web.ViewModels.Manufacturer;
using Microsoft.EntityFrameworkCore;
using static Epsilon.Data.Common.DataValidation;

namespace Epsilon.Services.Data
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IDeletableEntityRepository<Manufacturer> manufacturerRepository;

        public ManufacturerService(IDeletableEntityRepository<Manufacturer> _manufacturerRepository)
        {
            manufacturerRepository = _manufacturerRepository;
        }

        public async Task CreateAsync(ManufacturerCreateInputModel model, string creatorId)
        {
            var manufacturer = new Manufacturer()
            {
                Name = model.Name,
                Country = model.Country,
            };

            await manufacturerRepository.AddAsync(manufacturer);
            await manufacturerRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int manufacturerId)
        {
            var manufacturer = await manufacturerRepository
               .All()
               .Where(c => c.Id == manufacturerId)
               .FirstOrDefaultAsync();

            if (manufacturer == null)
            {
                throw new ArgumentNullException();
            }

            manufacturerRepository.Delete(manufacturer);
            await manufacturerRepository.SaveChangesAsync();
        }

        public async Task EditByIdAsync(ManufacturerEditInputModel model, string creatorId)
        {
            var manufacturer = await manufacturerRepository
                .All()
                .Where(c => c.Id == model.Id)
                .FirstOrDefaultAsync();

            if (manufacturer == null)
            {
                throw new ArgumentNullException();
            }

            manufacturer.Name = model.Name;
            manufacturer.Country = model.Country;

            manufacturerRepository.Update(manufacturer);
            await manufacturerRepository.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync<T>()
        {
            return await manufacturerRepository
                .AllAsNoTracking()
                .OrderBy(m => m.Name)
                .To<T>()
                .ToListAsync();
        }

        public async Task<List<T>> GetAllWithDeletedAsync<T>()
        {
            return await manufacturerRepository
                .AllAsNoTrackingWithDeleted()
                .OrderBy(m => m.Name)
                .To<T>()
                .ToListAsync();
        }

        public async Task<T> GetOneByIdAsync<T>(int id)
        {
            return await manufacturerRepository
                .AllAsNoTracking()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }
    }
}
