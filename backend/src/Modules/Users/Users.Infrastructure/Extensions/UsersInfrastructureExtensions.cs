using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SharedFramework.Database;
using Users.Application.Services.Abstract;
using Users.Domain.Models;
using Users.Infrastructure.Data;
using Users.Infrastructure.Services;

namespace Users.Infrastructure.Extensions;

public static class UsersInfrastructureExtensions
{
    public static IServiceCollection AddUsersInfrastructure(this IServiceCollection services)
    {
        services.AddSqlite<UsersDbContext>();
        
        services.AddIdentityCore<UserModel>()
            .AddEntityFrameworkStores<UsersDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
        
        services.AddScoped<ITokensService, JwtTokensService>();
        services.AddScoped<INumericCodesService, NumericCodesService>();
        
        return services;
    }
}
