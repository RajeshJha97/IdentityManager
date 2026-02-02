namespace IdentityManager.Api.Models.Dto;

public class AuthDto
{
    public record UserRegistration(string Name,string Email,string Password,string ConfirmPassword);
    public record Login(string Email,string Password,bool RememberMe);
    public record EmailCofirmation(string Email, string Token);
    public record RemoveUser(string Email);
    public record ForgetPassword(string Email);
    public record ResetPassword(string Token,string Email,string NewPassword);
}
