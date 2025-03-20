using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedFramework.Credentials;
using SharedFramework.Extensions;

namespace SharedFramework.Email;

public static class EmailExtensions
{
    public static IServiceCollection AddEmailServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ICredentialsProvider credentialsProvider)
    {
        var serverAddress = configuration["Smtp:ServerAddress"];
        var serverPort = configuration.GetValue<int>("Smtp:ServerPort");
        var userAddress = credentialsProvider.GetAsync(CredentialType.SmtpUserAddress).WaitResult();
        var userPassword = credentialsProvider.GetAsync(CredentialType.SmtpUserPassword).WaitResult();

        services.Configure<SmtpConfig>(config =>
        {
            config.ServerAddress = serverAddress;
            config.ServerPort = serverPort;
            config.UserAddress = userAddress;
            config.UserPassword = userPassword;
        });

        services.AddScoped<IEmailSender, EmailSender>();

        return services;
    }
}
