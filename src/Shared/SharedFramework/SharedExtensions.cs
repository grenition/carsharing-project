using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedFramework.Database;

namespace SharedFramework;

public static class SharedExtensions
{
    public static IServiceCollection AddSharedFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);
        
        return services;
    }
    public static IApplicationBuilder UseSharedFramework(this IApplicationBuilder app)
    {
        
        
        return app;
    }
}
