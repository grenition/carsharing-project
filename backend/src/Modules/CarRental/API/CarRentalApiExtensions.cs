using CarRental.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.API;

public static class CarRentalApiExtensions
{
    public static IServiceCollection AddCarRentalModule(this IServiceCollection services)
    {
        services.AddCarRentalInfrastructure();
        
        return services;
    }

    public static IApplicationBuilder UseCarRentalModule(this IApplicationBuilder app)
    {
        return app;
    }
}