using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedFramework.Authentication.Configs;
using Users.Application.Factories.Abstract;
using Users.Domain.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Users.Infrastructure.Factories;

public class JwtTokenFactory : ITokenFactory
{
    private readonly IOptions<JwtConfig> _jwtConfig;

    public JwtTokenFactory(IOptions<JwtConfig> jwtConfig)
    {
        _jwtConfig = jwtConfig;
    }

    public Task<string> GenerateAuthToken(UserModel userModel)
    {
        var jwtKey = _jwtConfig.Value.Secret;
        var jwtIssuer = _jwtConfig.Value.ValidIssuer;
        var jwtAudience = _jwtConfig.Value.ValidAudience;
        var expirationMinutes = _jwtConfig.Value.TokenExpirationMinutes;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userModel.Id),
            new Claim(JwtRegisteredClaimNames.Email, userModel.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Task.FromResult(tokenString);
    }
}
