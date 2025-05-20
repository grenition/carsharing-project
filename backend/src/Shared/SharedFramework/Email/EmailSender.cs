using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SharedFramework.Application;

namespace SharedFramework.Email;

public class EmailSender : IEmailSender
{
    private readonly IOptions<SmtpConfig> _smtpConfig;
    private readonly IOptions<AppConfig> _appConfig;
    
    public EmailSender(IOptions<SmtpConfig> smtpConfig, IOptions<AppConfig> appConfig)
    {
        _smtpConfig = smtpConfig;
        _appConfig = appConfig;
    }
    
    public async Task Send(EmailContent content, string toAddress)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_appConfig.Value.Name, _smtpConfig.Value.UserAddress));
        emailMessage.To.Add(new MailboxAddress(toAddress, toAddress));
        emailMessage.Subject = content.Subject;
        emailMessage.Body = new TextPart("plain")
        {
            Text = content.Body
        };

        using var client = new SmtpClient();
        var securityOption = GetSecureSocketOption(_smtpConfig.Value.SecurityOption);

        await client.ConnectAsync(
            _smtpConfig.Value.ServerAddress,
            _smtpConfig.Value.ServerPort,
            securityOption
        );
        await client.AuthenticateAsync(_smtpConfig.Value.UserAddress, _smtpConfig.Value.UserPassword);
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
    
    private SecureSocketOptions GetSecureSocketOption(string? option) => option?.ToLower() switch
    {
        "none" => SecureSocketOptions.None,
        "starttls" => SecureSocketOptions.StartTls,
        "ssl" or "sslonconnect" => SecureSocketOptions.SslOnConnect,
        _ => SecureSocketOptions.Auto
    };
}
