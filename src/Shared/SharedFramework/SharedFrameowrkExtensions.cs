using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedFramework.Credentials;
using SharedFramework.Database;
using SharedFramework.Exceptions;
using SharedFramework.OpenAPI;

namespace SharedFramework;

public static class SharedFrameowrkExtensions
{
    public static IServiceCollection AddSharedFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddPostgres(configuration);
        services.AddOpenApiServices();
        services.AddDataProtection();
        services.AddErrorHandling();
        services.AddEnvCredentialsProvider();
        
        return services;
    }
    public static WebApplication UseSharedFramework(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseErrorHandling();
        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApiServices();
        }
        
        app.MapControllers();
        
        return app;
    }
}
