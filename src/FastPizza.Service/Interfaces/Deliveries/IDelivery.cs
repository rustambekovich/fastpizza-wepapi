using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Deliveries;
using FastPizza.Domain.Entities.Users;
using FastPizza.Service.Dtos.UserAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Interfaces.Deliveries
{
    public interface IDelivery
    {
        public Task<Delivery?> GetByIdAsync(long id);
        public Task<List<Delivery>> GetAllAsync(PaginationParams PaginationParams);
        public Task<bool> UpdateAsync(long id, RegisterUserDto dto);
        public Task<bool> DeleteAsync(long id);
        public Task<long> CountAsync();
    }
}
