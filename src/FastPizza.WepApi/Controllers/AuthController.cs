using FastPizza.Service.Dtos.Auth;
using FastPizza.Service.Interfaces.Auth;
using FastPizza.Service.Validators.Dtos;
using FastPizza.Service.Validators.Dtos.AuthValidatories;
using Microsoft.AspNetCore.Mvc;

namespace FastPizza.WepApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authservice;
        private readonly IAuthServiceSMS _authserviceSMS;

        public AuthController(IAuthService authService,
            IAuthServiceSMS authServiceSMS)
        {
            this._authservice = authService;
            this._authserviceSMS = authServiceSMS;
        }
  /*      [HttpPost("register/email")]
        /// email
        public async Task<IActionResult> RegisterByEmailAsync([FromForm] RegistrDto registrDto)
        {
            var result = await _authservice.RegisterAsync(registrDto);
            return Ok(new { result.Result, result.CachedMinutes });
        }*/

        /// sms
        [HttpPost("register")]

        public async Task<IActionResult> RegisterByPhoneAsync([FromForm] RegistrDto registrDto)
        {
            var validaor = new AuthValidatorRegistter();
            var resvalid = validaor.Validate(registrDto);
            if (resvalid.IsValid)
            {
                var result = await _authserviceSMS.RegisterAsync(registrDto);
                return Ok(new { result.Result, result.CachedMinutes });
            }
            else return BadRequest(resvalid.Errors);
           
        }

        /*[HttpPost("register/send-code/email")]
        public async Task<IActionResult> SendCodeRegisterByEmailAsync(string email)
        {
            var result = await _authservice.SendCodeForRegisterAsync(email);
            return Ok(new { result.Result, result.CachedVerificationMinutes });
        }*/

        [HttpPost("register/send-code")]
        public async Task<IActionResult> SendCodeRegisterByPhoneAsync(string phone)
        {
            var res = PhoneNumberValidator.IsValid(phone);
            if (res == false) return BadRequest("Phone number is invalid!");

            var result = await _authserviceSMS.SendCodeForRegisterAsync(phone);
            return Ok(new { result.Result, result.CachedVerificationMinutes });
        }

        /*[HttpPost("register/verify/email")]
        public async Task<IActionResult> VerifyRegisterByEmailAsync([FromBody] VerifyRegisterDto verifyRegisterDto)
        {
            var serviceResult = await _authservice.VerifyRegisterAsync(verifyRegisterDto.Email, verifyRegisterDto.Code);
            return Ok(new { serviceResult.Result, serviceResult.Token });
        }*/
        [HttpPost("register/verify")]

        public async Task<IActionResult> VerifyRegisterByPhoneAsync([FromBody] VerifyRegisterByPhoneDto verifyRegisterByPhoneDto)
        {
            var serviceResult = await _authservice.VerifyRegisterAsync(verifyRegisterByPhoneDto.Phone, verifyRegisterByPhoneDto.Code);
            return Ok(new { serviceResult.Result, serviceResult.Token });
        }

    }
}
