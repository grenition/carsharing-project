using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SharedFramework.Authentication.Jwt;
using SharedFramework.Credentials;

namespace SharedFramework.Authentication
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtBearerAuthentication(
            this IServiceCollection services,
            IConfiguration configuration,
            ICredentialsProvider credentialsProvider)
        {
            var jwtSecret = credentialsProvider
                .GetAsync(CredentialType.JwtKey)
                .GetAwaiter()
                .GetResult();
            var jwtIssuer = configuration["Jwt:Issuer"];
            var jwtAudience = configuration["Jwt:Audience"];
            var jwtExpirationMinutes = configuration.GetValue<int>("Jwt:ExpirationMinutes");
            
            services.Configure<JwtConfig>(config =>
            {
                config.Secret = jwtSecret;
                config.ValidIssuer = jwtIssuer;
                config.ValidAudience = jwtAudience;
                config.TokenExpirationMinutes = jwtExpirationMinutes;
            });

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var isInProduction = configuration["Environment"] != "Development";
            
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = isInProduction;
                    options.SaveToken = true;
                    options.UseSecurityTokenValidators = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = signingKey,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });

            return services;
        }
    }
}
