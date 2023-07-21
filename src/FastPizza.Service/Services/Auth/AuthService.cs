using FastPizza.DataAccess.Interfaces.Custumers;
using FastPizza.Domain.Exceptions.Customers;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.Auth;
using FastPizza.Service.Dtos.Security;
using FastPizza.Service.Interfaces.Auth;
using Microsoft.Extensions.Caching.Memory;

namespace FastPizza.Service.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ICustomerRepository _customerRepository;
    private const int CACHED_FOR_MINUTS_REGISTER = 60;
    private const int CACHED_FOR_MINUTS_VEFICATION = 5;

    public AuthService(IMemoryCache memoryCache, ICustomerRepository customerRepository)
    {
        this._memoryCache = memoryCache;
        this._customerRepository = customerRepository;
    }
    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegistrDto dto)
    {
        var Costumer = await _customerRepository.GetByEmailAsync(dto.Email);
        if (Costumer is not null) throw new CustomerAlreadyExsistExcaption(dto.Email);

        if(_memoryCache.TryGetValue(dto.Email, out RegistrDto registrDto))
        {
             registrDto.Email = registrDto.Email;
            _memoryCache.Remove(dto.Email);
        }
        else
        {
            _memoryCache.Set(dto.Email, dto, TimeSpan.FromMinutes(CACHED_FOR_MINUTS_REGISTER));
        }
        return (Result: true, CachedMinutes: CACHED_FOR_MINUTS_REGISTER);
    }

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string email)
    {
        if(_memoryCache.TryGetValue(email, out RegistrDto registrDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();
            verificationDto.Code = 1234;
            _memoryCache.Set(email, verificationDto, TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));

            // emal sende begin
            // emsil sender end 

            return (Result: true, CachedMinutes: CACHED_FOR_MINUTS_VEFICATION);
        }
        else
        {
            throw new CustomerExpiredException(); 
        }
    }

    public Task<(bool Result, string Token)> VerifyRegisterAsync(string email, int code)
    {
        throw new NotImplementedException();
    }
}
