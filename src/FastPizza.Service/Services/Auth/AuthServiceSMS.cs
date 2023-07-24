using FastPizza.DataAccess.Interfaces.Custumers;
using FastPizza.Domain.Exceptions.Customers;
using FastPizza.Service.Common.Helpers;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.Auth;
using FastPizza.Service.Dtos.Notifications;
using FastPizza.Service.Dtos.Security;
using FastPizza.Service.Interfaces.Auth;
using FastPizza.Service.Interfaces.Notifications;
using Microsoft.Extensions.Caching.Memory;

namespace FastPizza.Service.Services.Auth
{
    public class AuthServiceSMS : IAuthServiceSMS
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPhoneSender _phonesender;
        private const int CACHED_FOR_MINUTS_REGISTER = 60;
        private const int CACHED_FOR_MINUTS_VEFICATION = 5;

        private const string REGISTER_CACHE_KEY = "register_";
        private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
        private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;
        public AuthServiceSMS(IMemoryCache memoryCache, ICustomerRepository customerRepository,
                              IPhoneSender phonesender)
        {
            this._memoryCache = memoryCache;
            this._customerRepository = customerRepository;
            this._phonesender = phonesender;
        }
        public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegistrDto dto)
        {
            //var Costumer = await _customerRepository.GetByEmailAsync(dto.Email);
            var caostumerPhone = await _customerRepository.GetByPhoneAsync(dto.PhoneNumber);
            if (caostumerPhone is not null)
                throw new CustomerAlreadyExsistExcaption();

            if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.PhoneNumber, out RegistrDto registrDto))
            {
                registrDto.PhoneNumber = registrDto.PhoneNumber;
                _memoryCache.Remove(dto.PhoneNumber);
            }
            else
            {
                _memoryCache.Set(REGISTER_CACHE_KEY + dto.PhoneNumber, dto, TimeSpan.FromMinutes(CACHED_FOR_MINUTS_REGISTER));
            }
            return (Result: true, CachedMinutes: CACHED_FOR_MINUTS_REGISTER);
        }

        public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone)
        {
            if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegistrDto registrDto))
            {
                VerificationDto verificationDto = new VerificationDto();
                verificationDto.Attempt = 0;
                verificationDto.CreatedAt = TimeHelper.GetDateTime();
                verificationDto.Code = CodeGenerator.GenerateRandomNumber();
                _memoryCache.Set(phone, verificationDto, TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));

                // emal sende begin
                if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto oldVerifcationDto))
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
                }
                _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                    TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));
                // emsil sender end 

                PhoneMessage phoneMessage = new PhoneMessage();
                phoneMessage.Title = "Fast Pizza";
                phoneMessage.Content = "Your verification code : " + verificationDto.Code;
                phoneMessage.Recipent = phone.Substring(1);
                var result = await _phonesender.SenderAsync(phoneMessage);
                if (result is true) return (Result: true, CachedVerificationMinutes: CACHED_FOR_MINUTS_VEFICATION);
                else return (Result: false, CACHED_FOR_MINUTS_VEFICATION: 0);
            }
            else
            {
                throw new CustomerExpiredException();
            }
        }

        public Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
        {
            throw new NotImplementedException();
        }
    }
}
