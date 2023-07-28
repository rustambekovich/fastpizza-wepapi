using FastPizza.Service.Dtos.Auth;
using FastPizza.Service.Dtos.UserAuth;

namespace FastPizza.Service.Interfaces.UserAuth;

public interface IAuthUserServiceSMS
{
    public Task<(bool Result, int CachedMinutes)> RegisterUserAsync(RegisterUserDto dto);
    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeUserRegisterAsync(string phone);

    public Task<(bool Result, string Token)> VerifyUserRegisterAsync(string phone, int code);
    public Task<(bool Result, string Token)> LoginUserAsync(LoginUserdto userdto);

}
