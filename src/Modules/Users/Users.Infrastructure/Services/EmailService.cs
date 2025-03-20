using Users.Application.Services.Abstract;

namespace Users.Infrastructure.Services;

public class EmailService : IEmailService
{
    public async Task SendVerificationEmail(string to, string token)
    {
        Console.WriteLine("Confirmation token: " + token);
    }
    public async Task SendPasswordResetEmail(string to, string token)
    {
        Console.WriteLine("Password reset token: " + token);
    }
    public async Task SendTwoFactorCode(string to, string token)
    {
        Console.WriteLine("Two factor code: " + token);
    }
}
