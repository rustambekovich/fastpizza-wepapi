using FastPizza.Service.Dtos.BranchDto;
using FastPizza.Service.Dtos.ProductDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Validators.Dtos.BranchValidatories
{
    public class BranchValidator : AbstractValidator<BranchCreatDto>
    {
        public BranchValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().NotNull().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters");

            RuleFor(dto => dto.Latitude).InclusiveBetween(-90, 90)
                .WithMessage("Latitude noto'g'ri kiritilgan. Example:(-90, 90)");
            RuleFor(dto => dto.Longitude).InclusiveBetween(-180, 180)
                .WithMessage("Longitude noto'g'ri kiritilgan. Example:(-180, 180)");

        }
    }
}
