using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Categories;
using FastPizza.Domain.Entities.Products;
using FastPizza.Service.Dtos.CategoryDtos;
using FastPizza.Service.Dtos.ProductDtos;

namespace FastPizza.Service.Interfaces.Products;

public interface IProductServise
{
    public Task<bool> CreateAsync(ProductCreateDto dto);
    public Task<Product?> GetByIdAsync(long id);
    public Task<IList<Product>> GetAllAsync(PaginationParams PaginationParams);
    public Task<bool> UpdateAsync(long id, ProductCreateDto dto);
    public Task<bool> DeleteAsync(long id);
    public Task<long> CountAsync();
}
