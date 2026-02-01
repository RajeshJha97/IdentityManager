using FluentValidation;
using Microsoft.AspNetCore.Rewrite;
using static IdentityManager.Api.Models.Dto.AuthDto;

namespace IdentityManager.Api.Validator
{
    public class EmailConfirmationHandler:AbstractValidator<EmailCofirmation>
    {
        public EmailConfirmationHandler()
        {
            RuleFor(x => x)
                .NotNull().WithMessage("Request Body is empty.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email required.")
                .EmailAddress().WithMessage("Must be and email address.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token required for email confirmation.");
        }
    }
}
