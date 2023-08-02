using Dapper;
using FastPizza.DataAccess.Interfaces.OrderIteams;
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.DataAccess.Repositories.OrderIteams
{
    public class OrderItem : BaseRepository, IOrderIteam
    {
        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateAsync(List<Item> items)
        {
            try
            {
                await _connection.OpenAsync();
                int count = 0;
                foreach (var item in items)
                {
                    string query = "INSERT INTO public.order_items( order_id, product_id, quantity, total_price,  result_price, created_at, updated_at) " +
                        " VALUES ( @OrderId, @ProductId, @Quantity, @TotalPrice,  @ResultPrice, @CreatedAt, @UpdatedAt);";
                    var result = await _connection.ExecuteAsync(query, item);
                    count++;
                }
                return count;
            }
            catch 
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public Task<int> CreateAsync(Item entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Item>> GetAllAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<Item?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<(int ItemsCount, IList<Item>)> SearchAsync(string search, PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(long id, Item entity)
        {
            throw new NotImplementedException();
        }
    }
}
