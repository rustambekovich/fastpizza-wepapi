using FastPizza.Domain.Entities.Categories;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.CategoryDtos;
using FluentValidation;

namespace FastPizza.Service.Validators.Dtos;

public class CategoryValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters");
        RuleFor(dto => dto.Description).NotEmpty().NotNull().WithMessage("Description field is required!")
            .MinimumLength(20).WithMessage("Description must be more than 3 characters");
        int maxImageSizaMB = 3;

        RuleFor(dto => dto.ImagePath).NotEmpty().NotNull()
            .WithMessage("Image field is required");
        RuleFor(dto => dto.ImagePath.Length).LessThan(maxImageSizaMB * 1024 * 1024)
            .WithMessage($"Image must be less than {maxImageSizaMB} MB");
        RuleFor(dto => dto.ImagePath.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtantion().Contains(fileInfo.Extension);
        }).WithMessage($"extension error");

    }
}