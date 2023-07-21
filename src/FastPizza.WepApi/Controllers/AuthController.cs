using FastPizza.Service.Dtos.Auth;
using FastPizza.Service.Interfaces.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastPizza.WepApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authservice;

        public AuthController(IAuthService authService)
        {
            this._authservice = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegistrDto registrDto)
        {
            var result = await _authservice.RegisterAsync(registrDto);
            return Ok( new { result.Result, result.CachedMinutes });
        }

        [HttpPost("register/send-code")]
        public async Task<IActionResult> SendCodeRegisterAsync( string email)
        {
            var result = await _authservice.SendCodeForRegisterAsync(email);
            return Ok( new { result.Result, result.CachedVerificationMinutes });
        }
    }
}
