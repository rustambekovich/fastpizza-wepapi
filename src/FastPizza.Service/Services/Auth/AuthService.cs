using FastPizza.DataAccess.Interfaces.Custumers;
using FastPizza.Domain.Entities.Customers;
using FastPizza.Domain.Exceptions.Auth;
using FastPizza.Domain.Exceptions.Customers;
using FastPizza.Service.Common.Helpers;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.Auth;
using FastPizza.Service.Dtos.Notifications;
using FastPizza.Service.Dtos.Security;
using FastPizza.Service.Interfaces.Auth;
using FastPizza.Service.Interfaces.Common;
using FastPizza.Service.Interfaces.Notifications;
using Microsoft.Extensions.Caching.Memory;

namespace FastPizza.Service.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IFileService _fileservice;
    private readonly IMemoryCache _memoryCache;
    private readonly IEmailsender _emailSender;
    private readonly ICustomerRepository _customerRepository;
    private const int CACHED_FOR_MINUTS_REGISTER = 60;
    private const int CACHED_FOR_MINUTS_VEFICATION = 5;

    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;
#pragma warning disable
    public AuthService(IMemoryCache memoryCache, ICustomerRepository customerRepository,
        IEmailsender emailsender, IFileService fileService)
    {
        this._fileservice = fileService;
        this._memoryCache = memoryCache;
        this._emailSender = emailsender;
        this._customerRepository = customerRepository;
    }
    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegistrDto dto)
    {
        var Costumer = await _customerRepository.GetByEmailAsync(dto.Email);
        var caostumerPhone = await _customerRepository.GetByPhoneAsync(dto.PhoneNumber);
        if (Costumer is not null || caostumerPhone is not null)
            throw new CustomerAlreadyExsistExcaption(dto.Email);

        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.Email, out RegistrDto registrDto))
        {
            registrDto.Email = registrDto.Email;
            _memoryCache.Remove(dto.Email);
        }
        else
        {
            _memoryCache.Set(REGISTER_CACHE_KEY + dto.Email, dto, TimeSpan.FromMinutes(CACHED_FOR_MINUTS_REGISTER));
        }
        return (Result: true, CachedMinutes: CACHED_FOR_MINUTS_REGISTER);
    }

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string email)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + email, out RegistrDto registrDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();
            verificationDto.Code = CodeGenerator.GenerateRandomNumber();
            _memoryCache.Set(email, verificationDto, TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));

            // emal sende begin
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + email, out VerificationDto oldVerifcationDto))
            {
                _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + email);
            }
            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + email, verificationDto,
                TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));
            // emsil sender end 
            EmailMessage smsMessage = new EmailMessage();
            smsMessage.Title = "Fast Pizza";
            smsMessage.Content = "Your verification code : " + verificationDto.Code;
            smsMessage.Recipent = email;
            var result = await _emailSender.SenderAsync(smsMessage);
            if (result is true) return (Result: true, CachedVerificationMinutes: CACHED_FOR_MINUTS_VEFICATION);
            else return (Result: false, CachedMinutes: 0);
        }
        else
        {
            throw new CustomerExpiredException();
        }
    }

    public async Task<(bool Result, string Token)> VerifyRegisterAsync(string email, int code)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + email, out RegistrDto registerDto))
        {
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + email, out VerificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new VerificationTooManyRequestsException();
                else if (verificationDto.Code == code)
                {
                    var dbResult = await RegisterToDatabaseAsync(registerDto);
                    return (Result: dbResult, Token: "");
                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + email);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + email, verificationDto,
                        TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));
                    return (Result: false, Token: "");
                }
            }
            else throw new VerificationCodeExpiredException();
        }
        else throw new CustomerExpiredException();
    }


    private async Task<bool> RegisterToDatabaseAsync(RegistrDto registerDto)
    {
        //var imagePath = await _fileservice.UploadImageAsync(registerDto.ImagePathCustomer);
        Customer customer = new Customer();
        customer.FullName = registerDto.FullName;
        customer.Email = registerDto.Email;
        customer.CreatedAt = TimeHelper.GetDateTime();
        customer.UpdatedAt = TimeHelper.GetDateTime();
        customer.PhoneNumber = registerDto.PhoneNumber;


        var dbResult = await _customerRepository.CreateAsync(customer);
        return dbResult > 0;
    }


}
