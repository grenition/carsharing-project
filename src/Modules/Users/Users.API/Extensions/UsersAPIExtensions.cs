using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Extensions;
using Users.Infrastructure.Extensions;

namespace Users.API.Extensions;

public static class UsersAPIExtensions
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services)
    {
        services.AddUsersInfrastructure();
        services.AddUsersApplication();
        
        return services;
    }

    public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
    {
        return app;
    }
}

