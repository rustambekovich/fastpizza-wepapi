
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Categories;
using FastPizza.Service.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Interfaces.Categories
{
    public interface ICategoryService
    {
        public Task<bool> CreateAsync(CategoryCreateDto dto);
        public Task<Category?> GetByIdAsync(long id);
        public Task<List<Category>> GetAllAsync(PaginationParams PaginationParams);
        public Task<bool> UpdateAsync(long id, CategoryCreateDto dto);
        public Task<bool> DeleteAsync(long id);
        public Task<long> CountAsync();
    }
}
