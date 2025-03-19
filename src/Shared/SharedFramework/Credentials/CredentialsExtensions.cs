using Microsoft.Extensions.DependencyInjection;

namespace SharedFramework.Credentials;

public static class CredentialsExtensions
{
    public static IServiceCollection AddEnvCredentialsProvider(this IServiceCollection services, string path = ".env")
    {
        return services.AddSingleton<ICredentialsProvider>(_ => new EnvCredentialsProvider(path));
    }
}
