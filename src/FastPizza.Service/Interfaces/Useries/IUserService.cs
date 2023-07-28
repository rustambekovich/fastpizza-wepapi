using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Branches;
using FastPizza.Domain.Entities.Users;
using FastPizza.Service.Dtos.BranchDto;
using FastPizza.Service.Dtos.UserAuth;

namespace FastPizza.Service.Interfaces.Useries;

public interface IUserservice
{
    public Task<User?> GetByIdAsync(long id);
    public Task<List<User>> GetAllAsync(PaginationParams PaginationParams);
    public Task<bool> UpdateAsync(long id, RegisterUserDto dto);
    public Task<bool> DeleteAsync(long id);
    public Task<long> CountAsync();
}
