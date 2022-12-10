using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace Epsilon.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoryService(IDeletableEntityRepository<Category> _categoriesRepository)
        {
            categoriesRepository = _categoriesRepository;
        }

        public async Task CreateAsync(CategoryCreateInputModel inputModel, string creatorId)
        {
            var category = new Category()
            {
                Name = inputModel.Name,
            };

            await categoriesRepository.AddAsync(category);
            await categoriesRepository.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync<T>()
        {
            return await categoriesRepository
                .AllAsNoTrackingWithDeleted()
                .OrderBy(c => c.Name)
                .To<T>()
                .ToListAsync();
        }

        public async Task<T> GetOneByIdAsync<T>(int id)
        {
            return await categoriesRepository
                .AllAsNoTracking()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }
    }
}
