using FastPizza.DataAccess.Interfaces.Useries;
using FastPizza.Domain.Entities.Customers;
using FastPizza.Domain.Entities.Users;
using FastPizza.Domain.Enums;
using FastPizza.Domain.Exceptions.Auth;
using FastPizza.Domain.Exceptions.Customers;
using FastPizza.Domain.Exceptions.Users;
using FastPizza.Service.Common.Helpers;
using FastPizza.Service.Common.Security;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.Auth;
using FastPizza.Service.Dtos.Notifications;
using FastPizza.Service.Dtos.Security;
using FastPizza.Service.Dtos.UserAuth;
using FastPizza.Service.Interfaces.Auth;
using FastPizza.Service.Interfaces.Notifications;
using FastPizza.Service.Interfaces.UserAuth;
using FastPizza.Service.Services.Auth;
using Microsoft.Extensions.Caching.Memory;

namespace FastPizza.Service.Services.UserAuthService;

public class UserAuthServiceSMS : IAuthUserServiceSMS
{
    private readonly IMemoryCache _memoryCache;
    private readonly IUser _userRepository;
    private readonly IPhoneSender _phoneSender;
    private readonly ITokenUserService _tokenUserService;

    private const int CACHED_FOR_MINUTS_REGISTER = 60;
    private const int CACHED_FOR_MINUTS_VEFICATION = 5;

    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;

    public UserAuthServiceSMS(IMemoryCache memoryCache, IUser user,
        IPhoneSender phoneSender, ITokenUserService tokenUserService)
    {
        this._memoryCache = memoryCache;
        this._userRepository = user;
        this._phoneSender = phoneSender;
        this._tokenUserService = tokenUserService;

    }

    public async Task<(bool Result, int CachedMinutes)> RegisterUserAsync(RegisterUserDto dto)
    {
        var caostumerPhone = await _userRepository.GetByPhoneAsync(dto.PhoneNumber);
        if (caostumerPhone is not null)
            throw new UserAlreadyExsistExcaption();

        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.PhoneNumber, out RegisterUserDto registrDto))
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

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeUserRegisterAsync(string phone)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterUserDto registerUserDto))
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
            // emsil sender end //---------------------

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);


            //---------------------



            //---------------------

            PhoneMessage phoneMessage = new PhoneMessage();
            phoneMessage.Title = "Fast Pizza";
            phoneMessage.Content = "Your verification code : " + verificationDto.Code;
            phoneMessage.Recipent = phone.Substring(1);
            var result = await _phoneSender.SenderAsync(phoneMessage);
            if (result is true) return (Result: true, CachedVerificationMinutes: CACHED_FOR_MINUTS_VEFICATION);
            else return (Result: false, CACHED_FOR_MINUTS_VEFICATION: 0);
        }
        else
        {
            throw new UserExpiredException();
        }
    }

    public async Task<(bool Result, string Token)> VerifyUserRegisterAsync(string phone, int code)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterUserDto registerUserDto))
        {
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new VerificationTooManyRequestsException();
                else if (verificationDto.Code == code)
                {
                    var dbResult = await RegisterToDatabaseAsync(registerUserDto);
                    var result = await _userRepository.GetByPhoneAsync(phone);

                    if (result is null) throw new CustomerNotFoundException();

                    string token = _tokenUserService.GenereateToken(result);

                    return (Result: dbResult, Token: token);
                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                        TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));
                    return (Result: false, Token: "");
                }
            }
            else throw new VerificationCodeExpiredException();

        }
        return (Result: false, Token: "");


    }

    private async Task<bool> RegisterToDatabaseAsync(RegisterUserDto registerUserDto)
    {
        User user = new User();
        user.FirstName = registerUserDto.FirstName;
        user.LastName = registerUserDto.LastName;
        user.PhoneNumber = registerUserDto.PhoneNumber;
        user.PassportSeriaNumber = registerUserDto.PassportSeriaNumber;
        user.BithdayDate = registerUserDto.BithdayDate;
        user.MiddleName= registerUserDto.MiddleName;
        user.IsMale = registerUserDto.IsMale;
        user.IdentityRole = UserRole.User;
        user.WasBorn= registerUserDto.WasBorn;
        user.CreatedAt = user.UpdatedAt = TimeHelper.GetDateTime();
        //user.ImagePath = registerUserDto.ImagePath.ToString();
        var hasherResult = PasswordHasher.Hash(registerUserDto.Password);
        user.PasswordHash = hasherResult.Hash;
        user.Salt = hasherResult.Salt;

        var dbResult = await _userRepository.CreateAsync(user);
        return dbResult > 0;
    }
    public async Task<(bool Result, string Token)> LoginUserAsync(LoginUserdto userdto)
    {
        var user = await _userRepository.GetByPhoneAsync(userdto.PhoneNumber);
        if (user is null) throw new UserNotFoundException();

        var hasherResult = PasswordHasher.Verify(userdto.Password, user.PasswordHash, user.Salt);
        if (hasherResult == false) throw new PasswordNotMatchException();

        string token = _tokenUserService.GenereateToken(user);
        return (Result: true, Token: token);
    }
}
