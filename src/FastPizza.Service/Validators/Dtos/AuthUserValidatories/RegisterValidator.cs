using FastPizza.Service.Dtos.UserAuth;
using FluentValidation;

namespace FastPizza.Service.Validators.Dtos.AuthUserValidatories

{
    public class RegisterValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterValidator()
        {
            RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");
            RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("LastName is required!")
           .MinimumLength(3).WithMessage("LastName must be less than 3 characters")
           .MaximumLength(30).WithMessage("LastName must be less than 30 characters");
            RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required!")
          .MinimumLength(3).WithMessage("FirstName must be less than 3 characters")
          .MaximumLength(30).WithMessage("FirstName must be less than 30 characters");
            RuleFor(dto => dto.MiddleName).NotNull().NotEmpty().WithMessage("MiddleName is required!")
          .MinimumLength(3).WithMessage("MiddleName must be less than 3 characters")
          .MaximumLength(30).WithMessage("MiddleName must be less than 30 characters");
            RuleFor(dto => dto.PassportSeriaNumber).NotNull().NotEmpty().WithMessage("Passport Seria Number is required!")
          .MinimumLength(3).WithMessage("Passport Seria Number must be less than 9 characters")
          .MaximumLength(30).WithMessage("Passport Seria Number must be less than 9 characters");

            RuleFor(dto => dto.Password).Must(passsword => PasswordValidator.IsStrongPassword(passsword).IsValid)
                .WithMessage("Not strong password");
            RuleFor(dto => dto.WasBorn).NotNull().NotEmpty().WithMessage("WasBorn is required!")
                .WithMessage("WasBorn must be less than 3 characters")
          .MaximumLength(30).WithMessage("WasBorn must be less than 30 characters");


        }
    }
}