using FluentValidation;
using static IdentityManager.Api.Models.Dto.AuthDto;

namespace IdentityManager.Api.Validator
{
    public class LoginValidator:AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Email).NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Not a correct email format.");

            RuleFor(l => l.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
