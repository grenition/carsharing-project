using Users.API.Extensions;

namespace AppHost.Extensions;

public static class ModulesExtensions
{
    public static IServiceCollection AddModules(this IServiceCollection services)
    {
        services.AddUserModule();

        return services;
    }

    public static IEndpointRouteBuilder MapModules(this IEndpointRouteBuilder builder)
    {
        builder.MapUserEndpoints();

        return builder;
    }
}
