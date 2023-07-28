using Dapper;
using FastPizza.DataAccess.Interfaces.Useries;
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Products;
using FastPizza.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.DataAccess.Repositories.Useries
{
    public class UserRepository : BaseRepository, IUser
    {
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = "SELECT Count(*) FROM public.users ";
                var data = (await _connection.ExecuteScalarAsync<long>(query));
                return data;
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

        public async Task<int> CreateAsync(User entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.users( first_name, last_name, middle_name, phone_number, passport_seria_number, is_male, birth_date, was_born, password_hash, salt, image_path, identity_role, created_at, updated_at) " +
                    $"VALUES (@FirstName, @LastName, @MiddleName, @PhoneNumber, @PassportSeriaNumber, @IsMale, '{entity.BithdayDate.Year}-{entity.BithdayDate.Month}-{entity.BithdayDate.Day}', @WasBorn, @PasswordHash, @Salt, @ImagePath, @IdentityRole, @CreatedAt, @UpdatedAt);";
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
                string query = $"Delete FROM public.users where id = {id}";
                var data = await _connection.ExecuteAsync(query, new { Id = id });
                return data;
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

        public async Task<IList<User>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "SELECT * FROM public.users ORDER BY id DESC " +
                    $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
                var result = (await _connection.QueryAsync<User>(query)).ToList();
                return result;
            }
            catch
            {
                IList<User> result = new List<User>();
                return result;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }


        public async Task<User?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select * FROM public.users where id = {id}";
                var data = await _connection.QuerySingleAsync<User>(query, new { Id = id });
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

        public async Task<User?> GetByPhoneAsync(string phone)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "SELECT * FROM public.users where phone_number = @PhoneNumber";
                var data = await _connection.QuerySingleAsync<User>(query, new { PhoneNumber = phone });
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

        public Task<(int ItemsCount, IList<User>)> SearchAsync(string search, PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(long id, User entity)
        {
            try// first_name, last_name, middle_name, phone_number, passport_seria_number, is_male, birth_date, was_born, password_hash, salt, image_path, identity_role, created_at, updated_at
            { //@FirstName, @LastName, @MiddleName, @PhoneNumber, @PassportSeriaNumber, @IsMale, '{entity.BithdayDate.Year}-{entity.BithdayDate.Month}-{entity.BithdayDate.Day}', @WasBorn, @PasswordHash, @Salt, @ImagePath, @IdentityRole, @CreatedAt, @UpdatedAt);
                await _connection.OpenAsync();
                string query = "UPDATE public.users " +
                                "SET last_name = @LastName, middle_name = @MiddleName, " +
                                "phone_number = @PhoneNumber,passport_seria_number = @PassportSeriaNumber, is_male = @IsMale, " +
                                $"birth_date = '{entity.BithdayDate.Year}-{entity.BithdayDate.Month}-{entity.BithdayDate.Day}', " +
                                "password_hash = @PasswordHash, salt = @Salt, " +
                                "image_path = @ImagePath, identity_role = @IdentityRole,  updated_at = @UpdatedAt" +
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
