using FluentValidation;
using static IdentityManager.Api.Models.Dto.AuthDto;

namespace IdentityManager.Api.Validator;

public class RemoveUserValidator : AbstractValidator<RemoveUser>
{
    public RemoveUserValidator()
    {

        RuleFor(x => x)
            .NotNull()
            .WithMessage("Request body is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.");
    }
}
