
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Categories;
using FastPizza.Service.Dtos.CategoryDtos;

namespace FastPizza.Service.Interfaces.Categories
{
    public interface ICategoryService
    {
        public Task<bool> CreateAsync(CategoryCreateDto dto);
        public Task<Category?> GetByIdAsync(long id);
        public Task<List<Category>> GetAllAsync(PaginationParams PaginationParams);
        public Task<bool> UpdateAsync(long id, CategotryUpdatedDto dto);
        public Task<bool> DeleteAsync(long id);
        public Task<long> CountAsync();
    }
}
