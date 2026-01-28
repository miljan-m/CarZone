using CarZone.Application.DTOs.ModelDTOs;
using FluentValidation;

namespace CarZone.Application.Validation.UpdateValidation
{
    public class UpdateModelDTOValidator:AbstractValidator<UpdateModelDTO>
    {
        public UpdateModelDTOValidator()
        {
            RuleFor(m => m.ModelName).NotEmpty().WithMessage("Model name cannot be an empty string")
                                     .Matches("^[A-Z][a-zA-Z]*$").WithMessage("Model name must start with capital letter");
        }
    }
}