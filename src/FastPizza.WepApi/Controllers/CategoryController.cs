using FastPizza.DataAccess.Utils;
using FastPizza.Service.Dtos.CategoryDtos;
using FastPizza.Service.Interfaces.Categories;
using FastPizza.Service.Validators.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastPizza.WepApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly int maxPageSize = 30;

        public CategoryController(ICategoryService service)
        {
            this._categoryService = service; 
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CategoryCreateDto dto)
        {
            var categoryvalidator = new CategoryValidator();
            var validatorResult =  categoryvalidator.Validate(dto);
            if (validatorResult.IsValid)
                return Ok(await _categoryService.CreateAsync(dto));
            else return BadRequest(validatorResult.Errors);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _categoryService.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("categoryId")]
        public async Task<IActionResult> GetById(long categoryId)
            => Ok(await _categoryService.GetByIdAsync(categoryId));

        [HttpPut("categoryId")]
        public async Task<IActionResult> UpdateAsync(long CategoryId, [FromForm] CategoryCreateDto dto)
            => Ok(await _categoryService.UpdateAsync(CategoryId, dto));

        [HttpDelete("categoryId")]
        public async Task<IActionResult> DeletedAsync(long categoryId)
            => Ok(await _categoryService.DeleteAsync(categoryId));

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync()
            =>Ok(await _categoryService.CountAsync());
    }
}
