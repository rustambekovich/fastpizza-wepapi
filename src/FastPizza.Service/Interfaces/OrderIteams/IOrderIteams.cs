using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Items;
using FastPizza.Domain.Entities.Orders;
using FastPizza.Service.Dtos.OrdersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Interfaces.OrderIteams
{
    public interface IOrderIteams
    {
        public Task<bool> CreateAsync(List<Item> items);
        public Task<Item?> GetByIdAsync(long id);
        public Task<List<Item>> GetAllAsync(PaginationParams PaginationParams);
        public Task<bool> UpdateAsync(long id, Item item);
        public Task<bool> DeleteAsync(long id);
        public Task<long> CountAsync();
    }
}
