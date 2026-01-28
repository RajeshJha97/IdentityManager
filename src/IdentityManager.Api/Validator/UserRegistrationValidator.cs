using FluentValidation;
using static IdentityManager.Api.Models.Dto.AuthDto;

namespace IdentityManager.Api.Validator;

public class UserRegistrationValidator:AbstractValidator<UserRegistration>
{
    public UserRegistrationValidator()
    {
        RuleFor(u => u.Name).NotEmpty().WithMessage("Name is required.");

        RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Not a valid email.");

        RuleFor(u => u.Password).NotEmpty()
            .MinimumLength(6).WithMessage("Password must be atleast 5 characters")
            .MaximumLength(30).WithMessage("Password must not exceed 30 characters.");

        RuleFor(u => u.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword is required.")
            .Equal(u => u.Password).WithMessage("Password and ConfirmPassword must be same.");
    }
}
