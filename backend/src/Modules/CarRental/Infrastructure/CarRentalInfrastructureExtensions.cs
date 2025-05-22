using CarRental.Infrastructure.Data;
using CarRental.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedFramework.Data.Repositories;
using SharedFramework.Database;

namespace CarRental.Infrastructure;

public static class CarRentalInfrastructureExtensions
{
    public static IServiceCollection AddCarRentalInfrastructure(this IServiceCollection services)
    {
        services.AddSqlite<CarRentalDbContext>();

        services.AddScoped<DbContext, CarRentalDbContext>();

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        services.AddScoped<UserModelRepository>();
        services.AddScoped<CarModelRepository>();
        services.AddScoped<RentalModelRepository>();
        services.AddScoped<PaymentModelRepository>();

        return services;
    }
}