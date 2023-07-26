using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Branches;
using FastPizza.Service.Dtos.BranchDto;

namespace FastPizza.Service.Interfaces.Branches;

public interface IBranchService
{
    public Task<bool> CreateAsync(BranchCreatDto dto);
    public Task<Branch?> GetByIdAsync(long id);
    public Task<List<Branch>> GetAllAsync(PaginationParams PaginationParams);
    public Task<bool> UpdateAsync(long id, BranchCreatDto dto);
    public Task<bool> DeleteAsync(long id);
    public Task<long> CountAsync();
}
