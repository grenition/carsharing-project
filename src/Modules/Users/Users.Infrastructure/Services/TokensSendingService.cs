using SharedFramework.Email;
using Users.Application.Services.Abstract;

namespace Users.Infrastructure.Services;

public class TokensSendingService : ITokensSendingService
{
    private readonly IEmailSender _emailSender;
    public TokensSendingService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    
    public async Task SendAuthVerificationToken(string to, string token)
    {
        await _emailSender.Send(new EmailContent("Your confirmation token", token), to);
    }
    public async Task SendPasswordResetToken(string to, string token)
    {
        await _emailSender.Send(new EmailContent("Your password reset token", token), to);
    }
    public async Task SendTwoFactorCode(string to, string token)
    {
        await _emailSender.Send(new EmailContent("Your two factor authentication code", token), to);
    }
}
