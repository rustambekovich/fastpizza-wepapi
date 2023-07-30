using FastPizza.DataAccess.Interfaces;
using FastPizza.DataAccess.Interfaces.Useries;
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Users;
using FastPizza.Domain.Enums;
using FastPizza.Domain.Exceptions.Users;
using FastPizza.Service.Common.Security;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.UserAuth;
using FastPizza.Service.Interfaces.Common;
using FastPizza.Service.Interfaces.Useries;

namespace FastPizza.Service.Services.Useries;

public class UserService : IUserservice
{
    private IPaginator _pageator;
    private IUser _userRepository;

    public UserService(IUser userRepository,
        IPaginator paginator)
    {
        this._pageator = paginator;
        this._userRepository = userRepository;
    }
    public async Task<long> CountAsync()
    {
        var result = await _userRepository.CountAsync();
        return result;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var resfound = await _userRepository.GetByIdAsync(id);
        if (resfound is null)
        {
            throw new UserNotFoundException();
        }
        var result = await _userRepository.DeleteAsync(id);
        if (result > 0) return true;
        else return false;
    }

    public async Task<List<User>> GetAllAsync(PaginationParams PaginationParams)
    {
        var result = await _userRepository.GetAllAsync(PaginationParams);
        if (result is null)
        {
            throw new UserNotFoundException();
        }
        var count = await _userRepository.CountAsync();
        _pageator.Paginate(count, PaginationParams);
        return result.ToList();
    }

    public async Task<User> GetByIdAsync(long id)
    {
        var result = await _userRepository.GetByIdAsync(id);
        if (result is null)
        {
            throw new UserNotFoundException();
        }
        return result;
    }

    public async Task<bool> UpdateAsync(long id, RegisterUserDto registerUserDto)
    {
        var resfound = await _userRepository.GetByIdAsync(id);
        if (resfound is null)
        {
            throw new UserNotFoundException();
        }
        User user = new User();
        user.FirstName = registerUserDto.FirstName;
        user.LastName = registerUserDto.LastName;
        user.PhoneNumber = registerUserDto.PhoneNumber;
        user.PassportSeriaNumber = registerUserDto.PassportSeriaNumber;
        user.BithdayDate = registerUserDto.BithdayDate;
        user.MiddleName = registerUserDto.MiddleName;
        user.IsMale = registerUserDto.IsMale;
        user.IdentityRole = UserRole.User;
        user.WasBorn = registerUserDto.WasBorn;
        user.UpdatedAt = TimeHelper.GetDateTime();
        //user.ImagePath = registerUserDto.ImagePath.ToString();
        var hasherResult = PasswordHasher.Hash(registerUserDto.Password);
        user.PasswordHash = hasherResult.Hash;
        user.Salt = hasherResult.Salt;

        var dbresult = await _userRepository.UpdateAsync(id, user);
        return dbresult > 0;
    }
}
