using FastPizza.Domain.Entities.Customers;
using FastPizza.Service.Dtos.Auth;
using FastPizza.Service.Dtos.BranchDto;
using FastPizza.Service.Interfaces.Customeries;
using FastPizza.Service.Validators.Dtos;
using FastPizza.Service.Validators.Dtos.AuthValidatories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastPizza.WepApi.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _customerservice;
        private readonly int maxsize = 3;
        public CustomerController(ICustomer customer)
        {
            this._customerservice = customer;
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CountAsync()
            =>Ok(await _customerservice.CountAsync());

        [HttpGet("customerId")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _customerservice.GetByIdAsync(id));

        [HttpDelete("customerId")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletetAsync(long id)
            =>Ok(await _customerservice.DeleteAsync(id));

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            =>Ok(await _customerservice.GetAllAsync(new DataAccess.Utils.PaginationParams(page, maxsize)));

        [HttpPut("customerId")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdetedAsync(long id, [FromForm] RegistrDto dto)
        {
            var validaor = new AuthValidatorRegistter();
            var resvalid = validaor.Validate(dto);
            var res = EmailValidator.IsValid(dto.Email);
            if (res == false) return BadRequest("Email is invalid!");
            if (resvalid.IsValid)
            {
                return Ok(await _customerservice.UpdateAsync(id, dto));
            }
            else return BadRequest(resvalid.Errors);
        }
    }
}
