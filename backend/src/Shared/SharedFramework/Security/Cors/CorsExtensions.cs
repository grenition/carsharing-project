using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedFramework.Security.Cors;

public static class CorsExtensions
{
    private const string Cors = "Cors";
    private const string DefaultPolicy = "DefaultPolicy";

    public static IServiceCollection AddCors(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var frontendPolicy = configuration
            .GetSection($"{Cors}:{DefaultPolicy}")
            .Get<CorsPolicyConfig>();
        
        services.AddCors(options =>
        {
            if(frontendPolicy == null)
                return;
            
            options.AddPolicy(DefaultPolicy, builder =>
            {
                builder.WithOrigins(frontendPolicy.Origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }

    public static IApplicationBuilder UseCors(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseCors(DefaultPolicy);
        
        return app;
    }
}
