using FastPizza.Service.Dtos.UserAuth;
using FastPizza.Service.Interfaces.UserAuth;
using FastPizza.Service.Validators.Dtos;
using FastPizza.Service.Validators.Dtos.AuthUserValidatories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastPizza.WepApi.Controllers
{
    [Route("api/user-auth")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private IAuthUserServiceSMS _userAuth;

        public UserAuthController(IAuthUserServiceSMS authUserServiceSMS)
        {
            this._userAuth = authUserServiceSMS;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterUserDto registerUserDto)
        {
            var validator = new RegisterValidator();
            var result = validator.Validate(registerUserDto);
            if (result.IsValid)
            {
                var serviceResult = await _userAuth.RegisterUserAsync(registerUserDto);
                return Ok(new { serviceResult.Result, serviceResult.CachedMinutes });
            }
            else return BadRequest(result.Errors);
        }

        [HttpPost("register/send-code")]
        [AllowAnonymous]
        public async Task<IActionResult> SendCodeRegisterAsync(string phone)
        {
            var result = PhoneNumberValidator.IsValid(phone);
            if (result == false) return BadRequest("Phone number is invalid!");

            var serviceResult = await _userAuth.SendCodeUserRegisterAsync(phone);
            return Ok(new { serviceResult.Result, serviceResult.CachedVerificationMinutes });
        }

        [HttpPost("register/verify")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyRegisterAsync([FromBody] VerifyRegisterDto verifyRegisterDto)
        {
            var result = PhoneNumberValidator.IsValid(verifyRegisterDto.phone);
            if (result == false) return BadRequest("Phone number is invalid!");
            var serviceResult = await _userAuth.VerifyUserRegisterAsync(verifyRegisterDto.phone, verifyRegisterDto.Code);
            return Ok(new { serviceResult.Result, serviceResult.Token });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserdto loginDto)
        {

            var result = PhoneNumberValidator.IsValid(loginDto.PhoneNumber);
            if (result == false) return BadRequest("Phone number is invalid!");
            var serviceResult = await _userAuth.LoginUserAsync(loginDto);
            return Ok(new { serviceResult.Result, serviceResult.Token });
        }

    }
}
