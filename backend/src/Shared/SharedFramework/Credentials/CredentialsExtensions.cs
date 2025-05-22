using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedFramework.Credentials;

public static class CredentialsExtensions
{
    public static IServiceCollection AddEnvCredentialsProvider(
        this IServiceCollection services,
        IConfiguration configuration,
        out ICredentialsProvider credentialsProvider)
    {
        var path = configuration["Credentials:EnvFile"];
        credentialsProvider = new EnvCredentialsProvider(path!);
        return services.AddSingleton(credentialsProvider);
    }
    
    public static IServiceCollection AddJsonCredentialsProvider(
        this IServiceCollection services,
        IConfiguration configuration,
        out ICredentialsProvider credentialsProvider)
    {
        credentialsProvider = new AppSettingsCredentialsProvider(configuration);
        return services.AddSingleton(credentialsProvider);
    }
}
