using Azure.Identity;
using FluentValidation;
using IdentityManager.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IdentityManager.Api.Models.Dto.AuthDto;

namespace IdentityManager.Api.Handlers;

public class AuthHandler([FromServices]UserManager<ApplicationUser> userManager, [FromServices]SignInManager<ApplicationUser>signInManager,
    [FromServices] ILogger<AuthHandler> logger, 
    IValidator<UserRegistration>registerationValidator,IValidator<Login> loginValidator )
{
    public async Task<IResult> HandleUserRegistration(UserRegistration request)
    {
        logger.LogInformation("HandleUserRegistration Invoked: ");
        var validationResult = await registerationValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            logger.LogWarning("user validation fails, input is not in a correct format.");
            var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
            {
                Status=StatusCodes.Status400BadRequest,
                Title="One or More validation errors occured.",
                Detail="See the error property for details."
            };
            return Results.BadRequest(problemDetails);
        }
        var user = new ApplicationUser { 
        
            UserName=request.Email,
            Name=request.Name,
            Email=request.Email

        };
        var result=await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            logger.LogWarning("user validation fails, input is not in a correct format.");
            var errorDict = new Dictionary<string, string[]>();
            errorDict["error"] = result.Errors.Select(e => e.Description).ToArray();
            var userCreationProblemDetails = new HttpValidationProblemDetails(errorDict)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "One or More validation errors occured.",
                Detail = "See the error property for details."
            };
            return Results.BadRequest(userCreationProblemDetails);
        }
        
        return Results.Ok(new { message="User registered successfully",user});
    }

    public async Task<IResult> HandleLogin(Login request)
    {
        logger.LogInformation("Login handler invoked: ");
        var validationResult=await loginValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            logger.LogWarning("user validation fails, input is not in a correct format.");
            var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "One or More validation errors occured.",
                Detail = "See the error property for details."
            };

            return Results.BadRequest(problemDetails);
        }

        var signInUser=await signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, lockoutOnFailure: true);

        if (signInUser.IsLockedOut)
        {
            logger.LogWarning("user locked out, user attempted with wrong credentials more then 3 times.");
            var signInProblemDetails = new
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "User locked out due to multiple attempts with wrong credentials.",
                Detail = "User locked out due to multiple attempts with wrong credentials. Please try after 2 mints"
            };

            return Results.Ok(signInProblemDetails);
        }

        if (!signInUser.Succeeded)
        {
            logger.LogWarning("user validation fails, user attempted with wrong credentials.");
            var signInProblemDetails = new
            {
                Status=StatusCodes.Status401Unauthorized,
                Title="Incorrect email/password."
            };

            return Results.Ok(signInProblemDetails);
        }
        

        var user = await userManager.FindByEmailAsync(request.Email);       
        var success = new { 
        
            Status=StatusCodes.Status200OK,
            Title="User authenticated successfully.",
            UserDetails=new {
                user!.Id,
                UserName =user!.Name,
                user!.Email,                
            }
        };

        return Results.Ok(success);
    }
}
