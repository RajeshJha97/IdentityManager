namespace IdentityManager.Api.Models.Dto;

public class AuthDto
{
    public record UserRegistration(string Name,string Email,string Password,string ConfirmPassword);
    public record Login(string Email,string Password,bool RememberMe);
}
