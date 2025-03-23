namespace Users.Application.Services.Abstract;

public interface ITokensSendingService
{
    Task SendAuthVerificationToken(string to, string token);
    Task SendPasswordResetToken(string to, string token);
    Task SendTwoFactorCode(string to, string code);
}
