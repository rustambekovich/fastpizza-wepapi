using FastPizza.DataAccess.Interfaces.OrderIteams;
using FastPizza.DataAccess.Interfaces.Orders;
using FastPizza.DataAccess.Interfaces.Products;
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Items;
using FastPizza.Domain.Entities.Orders;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.OrdersDto;
using FastPizza.Service.Interfaces.Orders;
using FastPizza.Service.Interfaces.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Services.Orders
{
    public class OrderServise : IOrderService
    {
        private readonly IProductRepository _productrepository;
        private readonly IOrderIteam _otderitems;
        private readonly IOrderRepository _reposritory;

        public OrderServise(IOrderRepository repository, IOrderIteam orderIteam,
            IProductRepository productrep)
        {
            this._productrepository = productrep;
            this._otderitems = orderIteam;
            this._reposritory = repository;
        }
        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAsync(OrderDto dto)
        {
            Order order = new Order();
            order.CustomerId = dto.CustomerId;
            order.Latitude = dto.Latitude;
            order.Longitude = dto.Longitude;
            order.PaymentType = dto.PaymentType;
            order.OrderType = dto.OrderType;
            order.DeliveryId = 1;
            order.Status = Domain.Enums.OrderStatus.Padding;
            order.ProductsPrice = 0;
            order.DeliveryPrice = 0;
            order.ResultPrice = order.ProductsPrice + order.DeliveryPrice;
            order.IsPaid = true;
            order.Description = "";
            order.BranchID = 1;
            order.CreatedAt = order.UpdatedAt = TimeHelper.GetDateTime();
            var result =  await _reposritory.CreateAsync(order);
            if(result != 0)
            {
                List<Item> items = new List<Item>();
                var pruduct = (dto.ProductID).Distinct().ToList();
                for(var i = 0; i < pruduct.Count(); i++)
                {
                    var getIdproduct = await _productrepository.GetByIdAsync(pruduct[i]);
                    int count = 0;
                    for(var j = 0; j < dto.ProductID.Count(); j++)
                    {
                        if (pruduct[i] == dto.ProductID[j])
                            count ++;
                    }
                    Item item = new Item();
                    item.Quantity = count;
                    item.OrderID = result;
                    item.ProductID = pruduct[i];
                    item.TotalPrice = getIdproduct.Unitprice;
                    item.ResultPrice = getIdproduct.Unitprice * item.Quantity;
                    item.CreatedAt = item.UpdatedAt = TimeHelper.GetDateTime();
                    items.Add(item);
                }
                    var dbresult = _otderitems.CreateAsync(items);
                return true;
            }
            else { return false; }
        }

        public Task<bool> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetAllAsync(PaginationParams PaginationParams)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(long id, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
