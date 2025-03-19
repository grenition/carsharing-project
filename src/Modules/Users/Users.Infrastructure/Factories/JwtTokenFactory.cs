using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedFramework.Credentials;
using Users.Application.Factories.Abstract;
using Users.Domain.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Users.Infrastructure.Factories;

public class JwtTokenFactory : ITokenFactory
{
    private readonly IConfiguration _configuration;
    private readonly ICredentialsProvider _credentialsProvider;

    public JwtTokenFactory(
        IConfiguration configuration,
        ICredentialsProvider credentialsProvider)
    {
        _configuration = configuration;
        _credentialsProvider = credentialsProvider;
    }

    public async Task<string> GenerateAuthToken(UserModel userModel)
    {
        var jwtKey = await _credentialsProvider.GetAsync(CredentialType.JwtKey);
        var jwtIssuer = _configuration["Jwt:Issuer"];
        var jwtAudience = _configuration["Jwt:Audience"];
        var expirationMinutes = _configuration.GetValue<int>("Jwt:ExpirationMinutes");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
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
        return tokenString;
    }
}
