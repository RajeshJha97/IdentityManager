using IdentityManager.Api.Handlers;
using Microsoft.AspNetCore.Mvc;
using static IdentityManager.Api.Models.Dto.AuthDto;

namespace IdentityManager.Api.Endpoints;

public static class ConfigureAuth
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/user/register", async (UserRegistration request, [FromServices]AuthHandler _handler) 
            => await _handler.HandleUserRegistration(request));

        app.MapPost("/api/user/login", async(Login request, [FromServices]AuthHandler _handler)
            => await _handler.HandleLogin(request));

        app.MapPost("/api/user/emailconfirmation", async (EmailCofirmation request, [FromServices] AuthHandler _handler)
           => await _handler.HandleEmailConfirmation(request));

        app.MapDelete("/api/user/remove", async ([FromBody] RemoveUser request, [FromServices] AuthHandler _handler)
            => await _handler.HandleRemoveUser(request));
    }
}
