using Dapper;
using FastPizza.DataAccess.Interfaces.Custumers;
using FastPizza.DataAccess.Utils;
using FastPizza.DataAccess.ViewModels.Users;
using FastPizza.Domain.Entities.Categories;
using FastPizza.Domain.Entities.Customers;

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


        public async Task<IList<CostumerViewModel>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "Select * from public.categories order by id desc " +
                    $"offset {@params.GetSkipCount()} limit {@params.PageSize}";
                var result = (await _connection.QueryAsync<CostumerViewModel>(query)).ToList();
                return result;
            }
            catch
            {
                IList<CostumerViewModel> result = new List<CostumerViewModel>();
                return result;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }


        public async Task<CostumerViewModel?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "SELECT * FROM public.customers where id = @Id;";
                var data = await _connection.QuerySingleOrDefaultAsync<CostumerViewModel>(query, new { Id = id });
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

        public Task<(int ItemsCount, IList<CostumerViewModel>)> SearchAsync(string search, PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(long id, Customer entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer?> GetByPhoneAsync(string phone)
        {
            try
            {

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
