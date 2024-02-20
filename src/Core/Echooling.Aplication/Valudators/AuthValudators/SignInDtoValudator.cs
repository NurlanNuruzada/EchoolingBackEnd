using Echooling.Aplication.DTOs.AuthDTOs;
using FluentValidation;

namespace Echooling.Aplication.Valudators.AuthValudators;
public class SignInDtoValudator:AbstractValidator<SignInDto>
{
    public SignInDtoValudator()
    {
        RuleFor(X => X.EmailOrUsername).NotNull().NotEmpty().MaximumLength(255);
        RuleFor(x => x.password).NotNull().NotEmpty().MaximumLength(255);
    }
}
