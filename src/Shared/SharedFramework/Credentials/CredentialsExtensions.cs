using Microsoft.Extensions.DependencyInjection;

namespace SharedFramework.Credentials;

public static class CredentialsExtensions
{
    public static IServiceCollection AddEnvCredentialsProvider(
        this IServiceCollection services,
        out ICredentialsProvider credentialsProvider,
        string path = ".env")
    {
        credentialsProvider = new EnvCredentialsProvider(path);
        return services.AddSingleton(credentialsProvider);
    }
}
