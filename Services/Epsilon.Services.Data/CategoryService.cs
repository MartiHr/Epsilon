using Epsilon.Data.Common.Repositories;
using Epsilon.Data.Models;
using Epsilon.Services.Data.Contracts;
using Epsilon.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepo;

        public CategoryService(IDeletableEntityRepository<Category> _categoriesRepo)
        {
            categoriesRepo = _categoriesRepo;
        }

        public Task CreateAsync(string categoryName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync<T>()
        {
            return await categoriesRepo
                .AllAsNoTracking()
                .To<T>()
                .ToListAsync();
        }
    }
}
