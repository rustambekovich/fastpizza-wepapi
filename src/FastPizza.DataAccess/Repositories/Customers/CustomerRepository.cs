using Dapper;
using FastPizza.DataAccess.Interfaces.Custumers;
using FastPizza.DataAccess.Utils;
using FastPizza.DataAccess.ViewModels.Users;
using FastPizza.Domain.Entities.Customers;

namespace FastPizza.DataAccess.Repositories.Customers
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"Select Count(*) from public.customers";
                return await _connection.ExecuteScalarAsync<long>(query);
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

        public async Task<int> CreateAsync(Customer entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.customers(full_name, phone_number, image_path_customer, created_at, updated_at, email) " +
                    "VALUES (@FullName, @PhoneNumber, @ImagePathCustomer, @CreatedAt, @UpdatedAt, @Email);";
                var result = await _connection.ExecuteAsync(query, entity);
                return result;
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


        public async Task<IList<Customer>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "Select * from public.customers order by id desc " +
                    $"offset {@params.GetSkipCount()} limit {@params.PageSize}";
                var result = (await _connection.QueryAsync<Customer>(query)).ToList();
                return result.ToList() ;
            }
            catch
            {
                IList<Customer> result = new List<Customer>();
                return result;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }


        public async Task<Customer?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "SELECT * FROM public.customers where id = @Id;";
                var data = await _connection.QuerySingleOrDefaultAsync<Customer>(query, new { Id = id });
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

        public Task<(int ItemsCount, IList<Customer>)> SearchAsync(string search, PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(long id, Customer entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "UPDATE public.customers " +
                                "SET  full_name = @FullName, phone_number = @PhoneNumber, image_path_customer = @ImagePathCustomer, email = @Email,  updated_at = @UpdatedAt" +
                               $" WHERE id = {id};";
                var result = await _connection.ExecuteAsync(query, entity);
                return result;
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

        public async Task<Customer?> GetByPhoneAsync(string phone)
        {
            try
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    await _connection.CloseAsync();

                }
                await _connection.OpenAsync();
                string query = "SELECT * FROM public.customers where phone_number = @PhoneNumber";
                var data = await _connection.QuerySingleOrDefaultAsync<Customer>(query, new { PhoneNumber = phone });
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
    }
}
