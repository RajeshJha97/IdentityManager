using Microsoft.AspNetCore.Identity;

namespace IdentityManager.Api.Models;

public class ApplicationUser:IdentityUser
{
    public string? Name { get; set; }
}
