using System.Collections.Generic;
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
        private readonly IPartService partService;
        
        public ComputerService(IDeletableEntityRepository<Computer> _computerRepository,
            IDeletableEntityRepository<Part> _partRepository,
            IPartService _partService)
        {
            computerRepository = _computerRepository;
            partService = _partService;
        }

        public async Task CreateAsync(ComputerCreateInputModel model, string creatorId)
        {
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

            //foreach (var image in model.Images)
            //{
            //    var imageModel = new Image()
            //    {
            //        CreatorId = creatorId,

            //    };
            //}

            await computerRepository.AddAsync(computer);
            await computerRepository.SaveChangesAsync();

            var partIds = new int[]
            {
                model.GPUId,
                model.CPUId,
                model.StorageId,
            };

            foreach (var partId in partIds)
            {
                await partService.AssignComputerToPart(computer, partId);
            }
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
