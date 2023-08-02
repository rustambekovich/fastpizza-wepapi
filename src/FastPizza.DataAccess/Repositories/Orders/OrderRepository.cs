using Dapper;
using FastPizza.DataAccess.Interfaces.Orders;
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.DataAccess.Repositories.Orders
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateAsync(Order entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.orders( customer_id,  status, products_price, delivery_price, " +
                    " result_price, latitude, longitude, payment_type, is_paid, description, order_type, branch_id, created_at, updated_at) " +
                    "VALUES ( @CustomerId,  @Status, @ProductsPrice, @DeliveryPrice, @ResultPrice, @Latitude, @Longitude, " +
                    " @PaymentType, @IsPaid, @Description, @OrderType, @BranchId, @CreatedAt, @UpdatedAt) RETURNING id ;";
                var dbresult = await _connection.ExecuteScalarAsync<int>(query, entity);
                return dbresult;
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

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Order>> GetAllAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<(int ItemsCount, IList<Order>)> SearchAsync(string search, PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(long id, Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
