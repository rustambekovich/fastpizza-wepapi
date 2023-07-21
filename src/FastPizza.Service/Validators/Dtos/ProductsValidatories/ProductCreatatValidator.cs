using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.ProductDtos;
using FluentValidation;

namespace FastPizza.Service.Validators.Dtos.ProductsValidatories;

public class ProductCreatatValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreatatValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters");
        RuleFor(dto => dto.Description).NotEmpty().NotNull().WithMessage("Description field is required!")
            .MinimumLength(20).WithMessage("Description must be more than 3 characters");
        int maxImageSizaMB = 3;

        RuleFor(dto => dto.Image).NotEmpty().NotNull()
            .WithMessage("Image field is required");
        RuleFor(dto => dto.Image.Length).LessThan(maxImageSizaMB * 1024 * 1024)
            .WithMessage($"Image must be less than {maxImageSizaMB} MB");
        RuleFor(dto => dto.Image.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtantion().Contains(fileInfo.Extension);
        }).WithMessage($"extension error");
    }
}
