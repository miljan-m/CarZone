using CarZone.Application.DTOs.ListingDTOs;
using FluentValidation;

namespace CarZone.Application.Validation.UpdateValidation
{
    public class UpdateListingDTOValidator : AbstractValidator<UpdateListingDTO>
    {
        public UpdateListingDTOValidator()
        {
            RuleFor(l => l.FuelConsuption).NotEmpty().WithMessage("Fuel consumption must be entered")
                                         .GreaterThan(0).WithMessage("Fuel consumption must be greater than 0")
                                         .LessThanOrEqualTo(50).WithMessage("Fuel consumption cannot be grater than 50L/100km");

            RuleFor(l => l.Mileage).GreaterThanOrEqualTo(0).WithMessage("Mileage consumption must be greater than 0")
                                    .LessThanOrEqualTo(1500000).WithMessage("Mileage cannot be greater than 1,000,000 km");

            RuleFor(l => l.ProductionYear).InclusiveBetween(1885, DateTime.Now.Year + 1).WithMessage($"Production year must be between 1885. and {DateTime.Now.Year + 1}");

            RuleFor(l => l.Price).GreaterThan(0).WithMessage("Vehicle price must be greater than 0");

            RuleFor(l => l.ModelId).GreaterThan(0).WithMessage("Vehicle model must already exist");
        }
    }
}