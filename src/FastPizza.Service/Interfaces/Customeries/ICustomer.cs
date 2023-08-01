using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Customers;
using FastPizza.Service.Dtos.Auth;

namespace FastPizza.Service.Interfaces.Customeries;

public interface ICustomer
{
    public Task<Customer?> GetByIdAsync(long id);
    public Task<List<Customer>> GetAllAsync(PaginationParams PaginationParams);
    public Task<bool> UpdateAsync(long id, RegistrDto dto);
    public Task<bool> DeleteAsync(long id);
    public Task<long> CountAsync();
}
