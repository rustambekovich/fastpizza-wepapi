using FastPizza.DataAccess.Utils;
using FastPizza.Service.Dtos.UserAuth;
using FastPizza.Service.Interfaces.Useries;
using FastPizza.Service.Validators.Dtos.AuthUserValidatories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FastPizza.WepApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserservice _servise;
        private readonly int maxsize = 30;

        public UserController(IUserservice userservice)
        {
            this._servise = userservice;
        }

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync()
            => Ok(await _servise.CountAsync());

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _servise.GetAllAsync(new PaginationParams(page, maxsize)));

        [HttpGet("userId")]
        public async Task<IActionResult> GetByIdAsync( long id)
            => Ok(await _servise.GetByIdAsync(id));
        [HttpPut("userId")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatedAsync(long id, [FromForm] RegisterUserDto registerUserDto)
        {
            var validator = new RegisterValidator();
            var result = validator.Validate(registerUserDto);
            if (result.IsValid)
            {
                var serviceResult = await _servise.UpdateAsync(id, registerUserDto);
                return Ok(await _servise.UpdateAsync(id, registerUserDto));
            }
            else return BadRequest(result.Errors);
        }

        [HttpDelete("userId")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletedAsync(long id)
            =>Ok(await _servise.DeleteAsync(id));
    }
}
