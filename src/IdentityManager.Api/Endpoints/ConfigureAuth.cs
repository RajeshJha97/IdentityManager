using IdentityManager.Api.Handlers;
using Microsoft.AspNetCore.Mvc;
using static IdentityManager.Api.Models.Dto.AuthDto;

namespace IdentityManager.Api.Endpoints;

public static class ConfigureAuth
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(("/api/user/register"), async ([FromBody]UserRegistration request, [FromServices]AuthHandler _handler) 
            => await _handler.HandleUserRegistration(request));
    }
}
