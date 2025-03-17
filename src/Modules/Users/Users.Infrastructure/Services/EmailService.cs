using Users.Application.Services.Abstract;

namespace Users.Infrastructure.Services;

public class EmailService : IEmailService
{
    public async Task SendVerificationEmailAsync(string to, string token)
    {
        Console.WriteLine("Confirmation token: " + token);
    }
    public Task SendPasswordResetEmailAsync(string to, string token)
    {
        throw new NotImplementedException();
    }
    public Task SendTwoFactorCodeAsync(string to, string code)
    {
        throw new NotImplementedException();
    }
}
