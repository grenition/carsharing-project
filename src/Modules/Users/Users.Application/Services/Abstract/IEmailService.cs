namespace Users.Application.Services.Abstract;

public interface IEmailService
{
    Task SendVerificationEmail(string to, string token);
    Task SendPasswordResetEmail(string to, string token);
    Task SendTwoFactorCode(string to, string token);
}
