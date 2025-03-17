namespace Users.Application.Services.Abstract;

public interface IEmailService
{
    Task SendVerificationEmailAsync(string to, string token);
    Task SendPasswordResetEmailAsync(string to, string token);
    Task SendTwoFactorCodeAsync(string to, string code);
}
