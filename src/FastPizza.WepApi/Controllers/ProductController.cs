using FastPizza.DataAccess.Utils;
using FastPizza.Service.Dtos.ProductDtos;
using FastPizza.Service.Interfaces.Categories;
using FastPizza.Service.Interfaces.Products;
using Microsoft.AspNetCore.Http;
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
            var result = Ok(await _service.CreateAsync(dto));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetALlAsync([FromQuery] int page = 1)
            =>Ok(await _service.GetAllAsync(new PaginationParams(page, maxSize)));
        [HttpGet("productId")]
        public async Task<IActionResult> GetByIdAsync(long id)
            =>Ok(await _service.GetByIdAsync(id));

        [HttpDelete("productId")]
        public async Task<IActionResult> DeleteByIdAsync(long id)
            =>Ok( await _service.DeleteAsync(id));

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync()
            =>Ok(await _service.CountAsync());
        [HttpPut("productId")]
        public async Task<IActionResult> UpdatedAsync(long id, [FromForm] ProductCreateDto dto)
            => Ok(await _service.UpdateAsync(id, dto));
    }
}
