using FluentValidation;
using static IdentityManager.Api.Models.Dto.AuthDto;

namespace IdentityManager.Api.Validator;

public class ForgetPasswordValidator:AbstractValidator<ForgetPassword>
{
    public ForgetPasswordValidator()
    {
        RuleFor(x => x)
            .NotNull().WithMessage("Request Body is missing.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email Required.")
            .EmailAddress().WithMessage("Incorrect email format.");
    }
}
