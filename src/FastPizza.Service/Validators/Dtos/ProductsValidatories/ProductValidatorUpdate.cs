using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.ProductDtos;
using FluentValidation;


namespace FastPizza.Service.Validators.Dtos.ProductsValidatories;

public class ProductValidatorUpdate : AbstractValidator<ProductUpdateDto>
{
    public ProductValidatorUpdate()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters");
        RuleFor(dto => dto.Description).NotEmpty().NotNull().WithMessage("Description field is required!")
            .MinimumLength(20).WithMessage("Description must be more than 3 characters");
        RuleFor(dto => dto.Unitprice).NotEmpty().NotNull().WithMessage("Unit must be entere");
        RuleFor(dto => dto.CategoryID).NotEmpty().NotNull().WithMessage("CategoryID must be entered ");
        When(dto => dto.Image is not null, () =>
        {
            int maxImageSizaMB = 3;
            RuleFor(dto => dto.Image.Length).LessThan(maxImageSizaMB * 1024 * 1024)
                .WithMessage($"Image must be less than {maxImageSizaMB} MB");
            RuleFor(dto => dto.Image.FileName).Must(predicate =>
            {
                FileInfo fileInfo = new FileInfo(predicate);
                return MediaHelper.GetImageExtantion().Contains(fileInfo.Extension);
            }).WithMessage($"extension error");
        });
    }
}
