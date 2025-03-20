using Users.Application.Services.Abstract;

namespace Users.Infrastructure.Services;

public class EmailService : IEmailService
{
    public async Task SendVerificationEmail(string to, string token)
    {
        Console.WriteLine("Confirmation token: " + token);
    }
    public Task SendPasswordResetEmail(string to, string token)
    {
        throw new NotImplementedException();
    }
    public Task SendTwoFactorCode(string to, string code)
    {
        throw new NotImplementedException();
    }
}
