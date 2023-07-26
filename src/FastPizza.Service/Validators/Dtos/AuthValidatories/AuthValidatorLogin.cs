using FastPizza.Service.Dtos.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Validators.Dtos.AuthValidatories
{
    public class AuthValidatorLogin : AbstractValidator<RegistrDto>
    {
        public AuthValidatorLogin()
        {
            RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");
        }
    }
}
