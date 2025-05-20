using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedFramework.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var appName = configuration["App:Name"];

        services.Configure<AppConfig>(config =>
        {
            config.Name = appName;
        });

        return services;
    }
}
