using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.Auth;
using FluentValidation;

namespace FastPizza.Service.Validators.Dtos.AuthValidatories;

public class AuthValidatorRegistter : AbstractValidator<RegistrDto>
{
    public AuthValidatorRegistter()
    {
        RuleFor(dto => dto.FullName).NotNull().NotEmpty().WithMessage("Firstname is required!")
            .MinimumLength(3).WithMessage("Firstname must be less than 3 characters")
            .MaximumLength(30).WithMessage("Firstname must be less than 30 characters");
       
        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");
    }
}
