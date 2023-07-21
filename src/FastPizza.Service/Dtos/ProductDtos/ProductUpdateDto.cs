using Microsoft.AspNetCore.Http;

namespace FastPizza.Service.Dtos.ProductDtos;

public class ProductUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }

    public float Unitprice { get; set; }
    public long CategoryID { get; set; }
}
