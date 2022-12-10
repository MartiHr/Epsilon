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
                .OrderByDescending(p => p.CreatedOn)
                .To<T>()
                .ToListAsync();
        }
    }
}
