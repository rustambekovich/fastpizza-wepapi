using FastPizza.Service.Dtos.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace FastPizza.Service.Validators.Dtos.AuthValidatories;

public class AuthValidatorRegistter : AbstractValidator<RegistrDto>
{
	public AuthValidatorRegistter()
	{
        RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("Firstname is required!")
            .MaximumLength(30).WithMessage("Firstname must be less than 30 characters");

        RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("Lastname is required!")
            .MaximumLength(30).WithMessage("Lastname must be less than 30 characters");
    }
}
