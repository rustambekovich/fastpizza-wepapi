using Dapper;
using FastPizza.DataAccess.Interfaces.Deliveries;
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Deliveries;
using FastPizza.Domain.Entities.Users;

namespace FastPizza.DataAccess.Repositories.Deliveries;

public class DeliveryRepository : BaseRepository, IDelivery
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT Count(*) FROM public.deliveries ";
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

    public async Task<int> CreateAsync(Delivery entity)

    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.deliveries( first_name, last_name, middle_name, phone_number, passport_seria_number, is_male, birth_date, was_born, password_hash, salt, image_path,  created_at, updated_at) " +
                $"VALUES (@FirstName, @LastName, @MiddleName, @PhoneNumber, @PassportSeriaNumber, @IsMale, '{entity.BithdayDate.Year}-{entity.BithdayDate.Month}-{entity.BithdayDate.Day}', @WasBorn, @PasswordHash, @Salt, @ImagePath, @CreatedAt, @UpdatedAt);";
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
            string query = $"Delete FROM public.deliveries where id = {id}";
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

    public async Task<IList<Delivery>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM public.users ORDER BY id DESC " +
                $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<Delivery>(query)).ToList();
            return result;
        }
        catch
        {
            IList<Delivery> result = new List<Delivery>();
            return result;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Delivery?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * FROM public.users where id = {id}";
            var data = await _connection.QuerySingleAsync<Delivery>(query, new { Id = id });
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

    public async Task<Delivery?> GetByPhoneAsync(string phone)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM public.users where phone_number = @PhoneNumber";
            var data = await _connection.QuerySingleAsync<Delivery>(query, new { PhoneNumber = phone });
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

    public Task<(int ItemsCount, IList<Delivery>)> SearchAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateAsync(long id, Delivery entity)
    {
        try// first_name, last_name, middle_name, phone_number, passport_seria_number, is_male, birth_date, was_born, password_hash, salt, image_path, identity_role, created_at, updated_at
        { //@FirstName, @LastName, @MiddleName, @PhoneNumber, @PassportSeriaNumber, @IsMale, '{entity.BithdayDate.Year}-{entity.BithdayDate.Month}-{entity.BithdayDate.Day}', @WasBorn, @PasswordHash, @Salt, @ImagePath, @IdentityRole, @CreatedAt, @UpdatedAt);
            await _connection.OpenAsync();
            string query = "UPDATE public.Delivery " +
                            "SET last_name = @LastName, middle_name = @MiddleName, " +
                            "phone_number = @PhoneNumber,passport_seria_number = @PassportSeriaNumber, is_male = @IsMale, " +
                            $"birth_date = '{entity.BithdayDate.Year}-{entity.BithdayDate.Month}-{entity.BithdayDate.Day}', " +
                            "password_hash = @PasswordHash, salt = @Salt, " +
                            "image_path = @ImagePath, updated_at = @UpdatedAt" +
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
