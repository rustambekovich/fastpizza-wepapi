using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Customers;
using FastPizza.Domain.Entities.Users;
using FastPizza.Service.Dtos.Auth;
using FastPizza.Service.Dtos.UserAuth;

namespace FastPizza.Service.Interfaces.Customeries;

public interface ICustomer
{
    public Task<Customer?> GetByIdAsync(long id);
    public Task<List<Customer>> GetAllAsync(PaginationParams PaginationParams);
    public Task<bool> UpdateAsync(long id, RegistrDto dto);
    public Task<bool> DeleteAsync(long id);
    public Task<long> CountAsync();
}
