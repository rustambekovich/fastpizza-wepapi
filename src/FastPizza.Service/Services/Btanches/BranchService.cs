using FastPizza.DataAccess.Interfaces.Branches;
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Branches;
using FastPizza.Domain.Exceptions.Branches;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.BranchDto;
using FastPizza.Service.Interfaces.Branches;

namespace FastPizza.Service.Services.Btanches
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IBranchRepository branchRepository)
        {
            this._branchRepository = branchRepository;
        }
        public async Task<long> CountAsync()
        {
           var count = await _branchRepository.CountAsync();
            if(count == 0) throw new BrenchNotFoundException();
            else
            {
                return count;
            }
        }

        public async Task<bool> CreateAsync(BranchCreatDto dto)
        {
           // var getres = await _branchRepository.GetByIdAsync(d)
            Branch branch = new Branch()
            {
                Name = dto.Name,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                CreatedAt = TimeHelper.GetDateTime(),
                UpdatedAt = TimeHelper.GetDateTime()
            };
            var result = await _branchRepository.CreateAsync(branch);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var resId = await _branchRepository.GetByIdAsync(id);
            if(resId == null)
            {
                throw new BrenchNotFoundException();
            }
            else
            {
                var result = await _branchRepository.DeleteAsync(id);
                return result > 0;
            }

        }

        public async Task<List<Branch>> GetAllAsync(PaginationParams PaginationParams)
        {
            var result = await _branchRepository.GetAllAsync(PaginationParams);
            if(result is null)
            {
                throw new BrenchNotFoundException();
            }
            return result.ToList();
        }

        public async Task<Branch> GetByIdAsync(long id)
        {
            var result = await _branchRepository.GetByIdAsync(id);
            if(result is null) throw new BrenchNotFoundException();
            else return result;
        }

        public async Task<bool> UpdateAsync(long id, BranchCreatDto dto)
        {
            var resId = await _branchRepository.GetByIdAsync(id);
            if(resId == null)
            {
                throw new BrenchNotFoundException();
            }
            else
            {
                resId.Name= dto.Name;
                resId.Longitude = dto.Longitude;
                resId.Latitude = dto.Latitude;
                resId.UpdatedAt = TimeHelper.GetDateTime();
            }
            var result = await _branchRepository.UpdateAsync(id, resId);
            if(result != 0) return true;
            else return false;
        }
    }
}
