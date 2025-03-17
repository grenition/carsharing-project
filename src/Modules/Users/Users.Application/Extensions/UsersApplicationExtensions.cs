using Microsoft.Extensions.DependencyInjection;
using Users.Application.Services.Abstract;
using Users.Application.Services.Implementations;

namespace Users.Application.Extensions;

public static class UsersApplicationExtensions
{
    public static IServiceCollection AddUsersApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRegistrationService, RegistrationService>();
        
        return services;
    }
}
