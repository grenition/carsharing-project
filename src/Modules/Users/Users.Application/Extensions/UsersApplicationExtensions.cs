using Microsoft.Extensions.DependencyInjection;
using Users.Application.Services;
using Users.Application.Services.Abstract;

namespace Users.Application.Extensions;

public static class UsersApplicationExtensions
{
    public static IServiceCollection AddUsersApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<IUsernameService, UsernameService>();
        
        return services;
    }
}
