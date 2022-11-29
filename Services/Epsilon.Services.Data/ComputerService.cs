using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Epsilon.Web.ViewModels.Computer;

namespace Epsilon.Services.Data
{
    public class ComputerService : IComputerService
    {
        private readonly IDeletableEntityRepository<Computer> computerRepository;

        public ComputerService(IDeletableEntityRepository<Computer> _computerRepository)
        {
            computerRepository = _computerRepository;
        }

        public async Task CreateAsync(ComputerCreateInputModel model, string creatorId)
        {
            var parts = (List<Part>)model.PartsGroups
                .Select(pg => pg.Parts
                    .Select(p => new Part() { Id = p.Id }));

            var computer = new Computer()
            {
                Name = model.Name,
                Model = model.Model,
                Price = model.Price,
                Description = model.Description,
                CategoryId = model.CategoryId,
                ManufacturerId = model.ManufacturerId,
                CreatorId = creatorId,
                Parts = parts,
            };

            await computerRepository.AddAsync(computer);
            await computerRepository.SaveChangesAsync();
        }
    }
}
