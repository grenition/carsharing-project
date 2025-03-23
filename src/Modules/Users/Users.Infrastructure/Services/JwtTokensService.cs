using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedFramework.Authentication.Configs;
using Users.Application.Services.Abstract;
using Users.Domain.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Users.Infrastructure.Services;

public class JwtTokensService : ITokensService
{
    private const string TwoFactorCodeClaimName = "two_factor_code";
    private const string TokenTypeClaimName = "type";
    
    private readonly JwtConfig _jwtConfig;
    private readonly TwoFactorConfig _twoFactorConfig;

    public JwtTokensService(
        IOptions<JwtConfig> jwtConfig,
        IOptions<TwoFactorConfig> twoFactorConfig)
    {
        _jwtConfig = jwtConfig.Value;
        _twoFactorConfig = twoFactorConfig.Value;
    }

    public Task<string> GenerateAuthToken(UserModel userModel)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_jwtConfig.Secret!);
        var securityKey = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userModel.Id),
            new Claim(JwtRegisteredClaimNames.Email, userModel.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        });

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.TokenExpirationMinutes),
            SigningCredentials = credentials,
            Issuer = _jwtConfig.ValidIssuer,
            Audience = _jwtConfig.ValidAudience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(securityToken);

        return Task.FromResult(tokenString);
    }
    public Task<string> GenerateTwoFactorToken(UserModel userModel, string encryptedCode)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_jwtConfig.Secret!);
        var securityKey = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userModel.Id),
            new Claim(JwtRegisteredClaimNames.Email, userModel.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(TokenTypeClaimName, "2fa"),
            new Claim(TwoFactorCodeClaimName, encryptedCode)
        });

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddMinutes(_twoFactorConfig.ExpirationMinutes),
            SigningCredentials = credentials,
            Issuer = _jwtConfig.ValidIssuer,
            Audience = _jwtConfig.ValidAudience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(securityToken);

        return Task.FromResult(tokenString);
    }
    public Task<string> GetTokenEmbeddedData(string token, TokenEmbeddedData data)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken;

        try
        {
            jwtToken = tokenHandler.ReadJwtToken(token);
        }
        catch
        {
            throw new SecurityTokenException("Invalid token format");
        }

        var claimType = data switch
        {
            TokenEmbeddedData.UserId => JwtRegisteredClaimNames.Sub,
            TokenEmbeddedData.UserEmail => JwtRegisteredClaimNames.Email,
            TokenEmbeddedData.TokenId => JwtRegisteredClaimNames.Jti,
            TokenEmbeddedData.TokenType => TokenTypeClaimName,
            TokenEmbeddedData.TwoFactorCode => TwoFactorCodeClaimName,
            _ => throw new ArgumentOutOfRangeException(nameof(data), data, "Unsupported embedded data type")
        };

        var claimValue = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;

        if (claimValue == null)
            throw new InvalidOperationException($"Claim '{claimType}' not found in token");

        return Task.FromResult(claimValue);
    }

}
