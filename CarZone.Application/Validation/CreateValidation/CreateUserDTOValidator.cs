using CarZone.Application.DTOs.UserDTOs;
using FluentValidation;

namespace CarZone.Application.Validation.CreateValidation
{
    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name cannot be an empty string")
                                   .Matches("^[A-Z][a-zA-Z]*$").WithMessage("First name must start with capital letter");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name cannot be an empty string")
                                   .Matches("^[A-Z][a-zA-Z]*$").WithMessage("Last name must start with capital letter");
            RuleFor(u => u.Address).NotEmpty().WithMessage("Address cannot be an empty string")
                                   .Matches(@"^([A-Z][a-zA-Z0-9]*|[A-Z][a-z]*\.)+(\s([A-Z][a-zA-Z0-9]*|[A-Z][a-z]*\.|\d+|[-]))*$").WithMessage("Address must start with capital letter");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email address cannot be an empty string")
                                    .EmailAddress().WithMessage("Email must be in form of example@example.com");
            RuleFor(u => u.Phone).NotEmpty().WithMessage("Phone number cannot be an empty string")
                                    .Must(u => u.StartsWith("06") && u.All(char.IsDigit) && u.Length == 10).WithMessage("Phone number must start with 06 and contain 10 digits");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password cannot be an empty string")
                                    .Matches(@"[A-Z]+").WithMessage("Password must contain at least one capital letter")
                                    .Matches(@"\d+").WithMessage("Password must contain at least one number")
                                    .Matches(@"[!@#$%^&*]+").WithMessage("Password must contain at least one speical caracter( ! @ # $ % ^ & *)");

        }
    }
}