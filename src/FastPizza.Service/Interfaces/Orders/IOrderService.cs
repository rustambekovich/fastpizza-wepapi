using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Categories;
using FastPizza.Domain.Entities.Orders;
using FastPizza.Service.Dtos.CategoryDtos;
using FastPizza.Service.Dtos.OrdersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Interfaces.Orders
{
    public interface IOrderService
    {
        public Task<bool> CreateAsync(OrderDto dto);
        public Task<Order?> GetByIdAsync(long id);
        public Task<List<Order>> GetAllAsync(PaginationParams PaginationParams);
        public Task<bool> UpdateAsync(long id, Order order);
        public Task<bool> DeleteAsync(long id);
        public Task<long> CountAsync();
    }
}
