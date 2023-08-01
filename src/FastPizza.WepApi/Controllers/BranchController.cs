using FastPizza.DataAccess.Utils;
using FastPizza.Service.Dtos.BranchDto;
using FastPizza.Service.Interfaces.Branches;
using FastPizza.Service.Validators.Dtos.BranchValidatories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastPizza.WepApi.Controllers
{
    [Route("api/branch")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly int maxsize = 3;
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            this._branchService = branchService;
        }

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync()
            => Ok(await _branchService.CountAsync());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatAsync([FromForm] BranchCreatDto dto)
        {
            var branchvalidator = new BranchValidator();
            var result = branchvalidator.Validate(dto);
            if (result.IsValid)
            {
                return Ok(await _branchService.CreateAsync(dto));
            }
            else return BadRequest(result.Errors);
        }


        [HttpGet("branch")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _branchService.GetAllAsync(new PaginationParams(page, maxsize)));

        [HttpGet("branchId")]
        public async Task<IActionResult> GetByIdasync(long id)
            => Ok(await _branchService.GetByIdAsync(id));

        [HttpDelete("branchId")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _branchService.DeleteAsync(id));

        [HttpPut("branchId")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] BranchCreatDto dto)
        {
            var branchvalidator = new BranchValidator();
            var result = branchvalidator.Validate(dto);
            if (result.IsValid)
            {
                return Ok(await _branchService.UpdateAsync(id, dto));
            }
            else return BadRequest(result.Errors);
        }

    }
}
