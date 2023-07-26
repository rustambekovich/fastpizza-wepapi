using Dapper;
using FastPizza.DataAccess.Interfaces.Branches;
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Branches;
using FastPizza.Domain.Entities.Categories;

namespace FastPizza.DataAccess.Repositories.Branches
{
    public class BranchRepository : BaseRepository, IBranchRepository
    {
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = "Select Count(*) from public.branch ";
                var result = (await _connection.ExecuteScalarAsync<long>(query));
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

        public async Task<int> CreateAsync(Branch entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.branch(name, latitude, longitude, created_at, updated_at) " +
                    "VALUES (@Name, @Latitude, @Longitude, @CreatedAt, @UpdatedAt);";
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
                string query = $"Delete from public.branch where id=@Id";
                var result = await _connection.ExecuteAsync(query, new { Id = id });
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

        public async Task<IList<Branch>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "Select * from public.branch order by id desc " +
                    $"offset {@params.GetSkipCount()} limit {@params.PageSize}";
                var result = (await _connection.QueryAsync<Branch>(query)).ToList();
                return result;
            }
            catch
            {
                IList<Branch> result = new List<Branch>();
                return result;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<Branch?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"Select * from public.branch where id=@Id";
                var result = await _connection.QuerySingleAsync<Branch>(query, new { Id = id });
                return result;
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

        public async Task<int> UpdateAsync(long id, Branch entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query =$"UPDATE public.branch " +
                    $"SET name = @Name, latitude = @Latitude, longitude = @Longitude, updated_at = @UpdatedAt " +
                    $"WHERE id = {id};";
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
    }
}
