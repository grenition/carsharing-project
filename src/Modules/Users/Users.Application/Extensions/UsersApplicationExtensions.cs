using Microsoft.Extensions.DependencyInjection;
using Users.Application.Factories;
using Users.Application.Factories.Abstract;
using Users.Application.Services;
using Users.Application.Services.Abstract;

namespace Users.Application.Extensions;

public static class UsersApplicationExtensions
{
    public static IServiceCollection AddUsersApplication(this IServiceCollection services)
    {
        services.AddScoped<IUsernameFactory, UsernameFactory>();
        
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<ITwoFactorSettingsService, TwoFactorSettingsService>();
        services.AddScoped<ITwoFactorAuthenticationService, TwoFactorAuthenticationService>();
        
        return services;
    }
}
