using FluentValidation;
using static IdentityManager.Api.Models.Dto.AuthDto;

namespace IdentityManager.Api.Validator
{
    public class ResetPasswordHandler : AbstractValidator<ResetPassword>
    {
        public ResetPasswordHandler()
        {
            RuleFor(x => x)
                .NotNull().WithMessage("Request Body is empty.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email required.")
                .EmailAddress().WithMessage("Must be and email address.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token required for email confirmation.");

            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New Password required for email confirmation.")
            .MinimumLength(6).WithMessage("Password must be atleast 5 characters")
            .MaximumLength(30).WithMessage("Password must not exceed 30 characters.");
        }
    }
}
