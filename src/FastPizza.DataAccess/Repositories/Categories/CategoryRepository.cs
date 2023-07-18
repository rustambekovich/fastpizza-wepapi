using Dapper;
using FastPizza.DataAccess.Interfaces.Categories;
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace FastPizza.DataAccess.Repositories.Categories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = "Select Count(*) from public.categories ";
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

        public async Task<int> CreateAsync(Category entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.categories( name, description, image_path, created_at, updated_at) " +
                    "VALUES (@Name, @Description, @ImagePath, @CreatedAt, @UpdatedAt);";
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
                string query = $"Delete from public.categories Where id=@Id";
                var result = await _connection.ExecuteAsync(query, new {Id = id});
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

        public async Task<IList<Category>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "Select * from public.categories order by id desc " +
                    $"offset {@params.GetSkipCount()} limit {@params.PageSize}";
                var result = (await _connection.QueryAsync<Category>(query)).ToList();
                return result;
            }
            catch
            {
               IList<Category> result = new List<Category>();
                return result;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<Category?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"Select * from public.categories where id=@Id";
                var result = await _connection.QuerySingleAsync<Category>(query,new {Id = id });
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

        public async Task<int> UpdateAsync(long id, Category entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "UPDATE public.categories " +
                                "SET  name = @Name, description = @Description, image_path = @ImagePath,  updated_at = @UpdatedAt" +
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
    }
}
