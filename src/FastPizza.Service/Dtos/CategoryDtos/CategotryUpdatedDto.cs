using Microsoft.AspNetCore.Http;

namespace FastPizza.Service.Dtos.CategoryDtos;

public class CategotryUpdatedDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? ImagePath { get; set; }
}
