using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedFramework.Application;
using SharedFramework.Authentication;
using SharedFramework.Credentials;
using SharedFramework.Database;
using SharedFramework.Email;
using SharedFramework.Exceptions;
using SharedFramework.OpenAPI;
using SharedFramework.Security.Cors;

namespace SharedFramework;

public static class SharedFrameworkExtensions
{
    public static IServiceCollection AddSharedFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSqlite(configuration);
        services.AddOpenApiServices();
        services.AddDataProtection();
        services.AddErrorHandling();
        services.AddJsonCredentialsProvider(configuration, out ICredentialsProvider credentialsProvider);
        services.AddBearerAuthentication(configuration, credentialsProvider);
        services.AddApplicationServices(configuration);
        services.AddEmailServices(configuration, credentialsProvider);
        services.AddCors(configuration);
        
        return services;
    }
    public static WebApplication UseSharedFramework(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors(app.Configuration);

        app.UseErrorHandling();
        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApiServices();
        }
        
        app.MapControllers();
        
        return app;
    }
}
