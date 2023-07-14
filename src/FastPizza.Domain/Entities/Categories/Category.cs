using System.ComponentModel.DataAnnotations;

namespace FastPizza.Domain.Entities.Categories;

public class Category
{
    [MinLength(1),MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

}
