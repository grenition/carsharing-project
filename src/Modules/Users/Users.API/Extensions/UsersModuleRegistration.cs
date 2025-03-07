using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Users.API.Extensions;

public static class UsersModuleRegistration
{
    public static IServiceCollection AddUserModule(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }

    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllers();
        return endpoints;
    }
}

