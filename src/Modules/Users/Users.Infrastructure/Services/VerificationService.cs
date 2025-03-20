using SharedFramework.Email;
using Users.Application.Services.Abstract;

namespace Users.Infrastructure.Services;

public class VerificationService : IVerificationService
{
    private readonly IEmailSender _emailSender;
    public VerificationService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    
    public async Task SendVerificationEmail(string to, string token)
    {
        await _emailSender.Send(new EmailContent("Your confirmation token", token), to);
    }
    public async Task SendPasswordResetEmail(string to, string token)
    {
        await _emailSender.Send(new EmailContent("Your password reset token", token), to);
    }
    public async Task SendTwoFactorCode(string to, string token)
    {
        await _emailSender.Send(new EmailContent("Your two factor authentication code", token), to);
    }
}
