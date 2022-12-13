using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Epsilon.Common;
using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Computer;
using Microsoft.EntityFrameworkCore;

namespace Epsilon.Services.Data
{
    public class ComputerService : IComputerService
    {
        private readonly IDeletableEntityRepository<Computer> computerRepository;
        private readonly IPartService partService;
        private readonly IImageService imageService;

        public ComputerService(IDeletableEntityRepository<Computer> _computerRepository,
            IPartService _partService,
            IImageService _imageService)
        {
            computerRepository = _computerRepository;
            partService = _partService;
            imageService = _imageService;
        }

        public async Task CreateAsync(ComputerCreateInputModel model, string creatorId, string imagePath)
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

            Directory.CreateDirectory($"{imagePath}/computers/");
            foreach (var image in model.Images)
            {
                string extension = Path.GetExtension(image.FileName);

                //if (!GlobalConstants.AllowedImageExtensions.Any(x => x.EndsWith(extension)))
                //{
                //    throw new Exception($"Invalid image extension {extension}");
                //}

                var imageModel = new Image()
                {
                    CreatorId = creatorId,
                    Computer = computer,
                    Extension = extension,
                };

                computer.Images.Add(imageModel);

                string physicalPath = $"{imagePath}/computers/{imageModel.Id}{extension}";

                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

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
                await partService.AssignComputerToExistingPartAsync(computer, partId);
            }
        }

        public async Task EditByIdAsync(ComputerEditInputModel model, string creatorId, string imagePath)
        {
            var computer = await GetOneByIdAsync(model.Id);

            if (model.Images != null && model.Images.Count > 0)
            {
                foreach (var image in computer.Images)
                {
                    await imageService.DeleteImageByIdAsync(image.Id);
                }

                Directory.CreateDirectory($"{imagePath}/computers/");
                foreach (var image in model.Images)
                {
                    string extension = Path.GetExtension(image.FileName);

                    var imageModel = new Image()
                    {
                        CreatorId = creatorId,
                        Computer = computer,
                        Extension = extension,
                    };

                    computer.Images.Add(imageModel);

                    string physicalPath = $"{imagePath}/computers/{imageModel.Id}{extension}";

                    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                    await image.CopyToAsync(fileStream);
                }
            }

            computer.Name = model.Name;
            computer.Model = model.Model;
            computer.Price = model.Price;
            computer.Description = model.Description;
            computer.CategoryId = model.CategoryId;
            computer.CreatorId = creatorId;
            computer.ManufacturerId = model.ManufacturerId;

            computerRepository.Update(computer);
            await computerRepository.SaveChangesAsync();

            foreach (var part in computer.Parts)
            {
                await partService.RemoveComputerFromExistingPartAsync(computer, part.Id);
            }

            var partIds = new int[]
            {
                model.GPUId,
                model.CPUId,
                model.StorageId,
            };

            foreach (var partId in partIds)
            {
                await partService.AssignComputerToExistingPartAsync(computer, partId);
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

        public async Task<List<T>> GetANumberOfAsync<T>(int count)
        {
            return await computerRepository
                .AllAsNoTracking()
                .OrderByDescending(c => c.CreatedOn)
                .Take(count)
                .To<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var computer = await computerRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            if (computer == null)
            {
                throw new Exception("No such computer exists.");
            }

            return computer;
        }

        public int GetCount()
        {
            return computerRepository
                .AllAsNoTracking()
                .Count();
        }

        public async Task<Computer> GetOneByIdAsync(int id)
        {
            return await computerRepository
                .All()
                .Where(c => c.Id == id)
                .Include(c => c.Parts)
                .Include(c => c.Images)
                .FirstOrDefaultAsync();
        }
    }
}
