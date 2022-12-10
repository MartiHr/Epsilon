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

        public async Task DeleteByIdAsync(int categoryId)
        {
            var dbCategory = await categoriesRepository
                .All()
                .Where(c => c.Id == categoryId)
                .FirstOrDefaultAsync();

            if (dbCategory == null)
            {
                throw new ArgumentNullException();
            }

            categoriesRepository.Delete(dbCategory);
            await categoriesRepository.SaveChangesAsync();
        }

        public async Task EditByIdAsync(CategoryEditInputModel inputModel, string creatorId)
        {
            // TODO: possibly add creator to category
            var dbCategory = await categoriesRepository
                .All()
                .Where(c => c.Id == inputModel.Id)
                .FirstOrDefaultAsync();

            if (dbCategory == null)
            {
                throw new ArgumentNullException();
            }

            dbCategory.Name = inputModel.Name;

            categoriesRepository.Update(dbCategory);
            await categoriesRepository.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync<T>()
        {
            return await categoriesRepository
                .AllAsNoTracking()
                .OrderBy(c => c.Name)
                .To<T>()
                .ToListAsync();
        }

        public async Task<List<T>> GetAllWithDeletedAsync<T>()
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
