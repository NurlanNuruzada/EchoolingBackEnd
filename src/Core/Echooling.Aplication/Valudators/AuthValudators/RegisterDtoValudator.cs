using Echooling.Aplication.DTOs.AuthDTOs;
using FluentValidation;

namespace Echooling.Aplication.Valudators.AuthValudators
{
    public class RegisterDtoValudator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValudator()
        {
            RuleFor(x => x.phoneNumber).MaximumLength(50);
            RuleFor(X => X.email).NotNull().NotEmpty().MaximumLength(255);
            RuleFor(x => x.surname).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.name).NotNull().NotEmpty().MaximumLength(55);
            RuleFor(x=>x.password).NotNull().NotEmpty().MaximumLength(255); 
            RuleFor(x => x.UserName) // Add validation rules for UserName
         .NotNull()
         .NotEmpty()
         .MaximumLength(256); // Adjust the maximum length as needed
        }
    }
}
