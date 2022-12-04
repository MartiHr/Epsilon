using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Epsilon.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Epsilon.Services.Data
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IDeletableEntityRepository<Manufacturer> manufacturerRepository;

        public ManufacturerService(IDeletableEntityRepository<Manufacturer> _manufacturerRepository)
        {
            manufacturerRepository = _manufacturerRepository;
        }

        public async Task<List<T>> GetAllAsync<T>()
        {
            return await manufacturerRepository
                .AllAsNoTracking()
                .OrderBy(m => m.Name)
                .To<T>()
                .ToListAsync();
        }
    }
}
