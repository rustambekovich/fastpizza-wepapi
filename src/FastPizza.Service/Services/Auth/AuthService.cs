using FastPizza.DataAccess.Interfaces.Custumers;
using FastPizza.Domain.Exceptions.Customers;
using FastPizza.Service.Dtos.Auth;
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

    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone)
    {
        throw new NotImplementedException();
    }

    public Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
    {
        throw new NotImplementedException();
    }
}
