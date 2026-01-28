using CarZone.Application.DTOs.BrandDTOs;
using FluentValidation;

namespace CarZone.Application.Validation.CreateValidation
{
    public class CreateBrandDTOValidator : AbstractValidator<CreateBrandDTO>
    {
        public CreateBrandDTOValidator()
        {
            RuleFor(b => b.BrandName).NotEmpty().WithMessage("Brand name cannot be an empty string")
                                     .Matches("^[A-Z][a-zA-Z]*$").WithMessage("Car brand must start with capital letter");
        }
    }
}