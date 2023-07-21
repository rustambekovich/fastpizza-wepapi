using Dapper;
using FastPizza.DataAccess.Interfaces.Custumers;
using FastPizza.DataAccess.Utils;
using FastPizza.DataAccess.ViewModels.Users;
using FastPizza.Domain.Entities.Customers;
using FastPizza.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.DataAccess.Repositories.Customers
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateAsync(Customer entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.customers(full_name, phone_number, image_path_customer, created_at, updated_at, email) " +
                    "VALUES (@FullName, @PhoneNnumber, @ImagePathCustomer, @CreatedAt, @UpdatedAt, @Email);";
                return await _connection.ExecuteAsync(query, entity);
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

        public async Task<int> DeleteAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"delete from public.customers where id = {id}";
                return await _connection.ExecuteAsync(query);
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

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "SELECT * FROM public.customers where email = @Email;";
                var data = await _connection.QuerySingleOrDefaultAsync<Customer>(query, new { Email = email });
                return data;
            }
            catch
            {
                return null;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }


        public Task<IList<CostumerViewModel>> GetAllAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        
        public Task<CostumerViewModel?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<(int ItemsCount, IList<CostumerViewModel>)> SearchAsync(string search, PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(long id, Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
