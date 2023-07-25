using FastPizza.DataAccess.Utils;
using FastPizza.Service.Dtos.ProductDtos;
using FastPizza.Service.Interfaces.Products;
using FastPizza.Service.Validators.Dtos.ProductsValidatories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastPizza.WepApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServise _service;
        private readonly int maxSize = 30;

        public ProductController(IProductServise service)
        {
            this._service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreatedAsync([FromForm] ProductCreateDto dto)
        {
            var Productvalidator = new ProductCreatatValidator();
            var validatorResult = Productvalidator.Validate(dto);
            if (validatorResult.IsValid)
                return Ok(await _service.CreateAsync(dto));
            else return BadRequest(validatorResult.Errors);
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> GetALlAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxSize)));
        [HttpGet("productId")]
        [AllowAnonymous]

        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpDelete("productId")]
        [AllowAnonymous]

        public async Task<IActionResult> DeleteByIdAsync(long id)
            => Ok(await _service.DeleteAsync(id));

        [HttpGet("count")]
        [AllowAnonymous]

        public async Task<IActionResult> CountAsync()
            => Ok(await _service.CountAsync());
        [HttpPut("productId")]
        [AllowAnonymous]

        public async Task<IActionResult> UpdatedAsync(long id, [FromForm] ProductUpdateDto dto)
        {
            var Productvalidator = new ProductValidatorUpdate();
            var validatorResult = Productvalidator.Validate(dto);
            if (validatorResult.IsValid) return Ok(await _service.UpdateAsync(id, dto));
            else return BadRequest(validatorResult.Errors);
        }
    }
}
