using FastPizza.DataAccess.Interfaces.OrderIteams;
using FastPizza.DataAccess.Interfaces.Products;
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Items;
using FastPizza.Service.Interfaces.OrderIteams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Services.Iteam
{
    public class OrderItemService : IOrderIteams
    {
        private readonly IOrderIteam _orderItemrepositopry;
        private readonly IProductRepository _productRepositpry;

        public OrderItemService(IProductRepository productRepository, 
            IOrderIteam orderIteam)
        {
            this._orderItemrepositopry = orderIteam;
            this._productRepositpry = productRepository;
        }
        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAsync(List<Item> items)
        {
            int count = 0;
            foreach(var item in items)
            {
                var pruduct = await _productRepositpry.GetByIdAsync(item.Id);
                item.TotalPrice = pruduct.Unitprice;
                item.ResultPrice = item.TotalPrice * item.Quantity;
                item.ProductID = pruduct.Id;
               
                 var dbresult = await _orderItemrepositopry.CreateAsync(items);
                if (dbresult != 0) count ++;
            }

            if(count != 0) return true; else return false;
        }

        public Task<bool> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Item>> GetAllAsync(PaginationParams PaginationParams)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(long id, Item item)
        {
            throw new NotImplementedException();
        }
    }
}
