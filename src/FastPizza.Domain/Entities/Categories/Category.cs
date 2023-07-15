using System.ComponentModel.DataAnnotations;

namespace FastPizza.Domain.Entities.Categories;

public class Category : Auditable
{
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

}
