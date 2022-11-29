using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Epsilon.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Epsilon.Services.Data
{
    public class PartService : IPartService
    {
        private readonly IDeletableEntityRepository<Part> partRepository;

        public PartService(IDeletableEntityRepository<Part> _partRepository)
        {
            partRepository = _partRepository;
        }

        public async Task<List<T>> GetAllAsync<T>()
        {
            return await partRepository
                .AllAsNoTracking()
                .To<T>()
                .ToListAsync();
        }

        public async Task<List<T>> GetAllOfTypeAsync<T>(string type)
        {
            return await partRepository
                .AllAsNoTracking()
                .Where(p => p.Type == type)
                .To<T>()
                .ToListAsync();
        }
    }
}
