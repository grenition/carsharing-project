using Users.Domain.Models;

namespace Users.Application.Services.Abstract;

public enum TokenEmbeddedData
{
    UserId,
    UserEmail,
    TokenId,
    TokenType,
    TwoFactorCode
}

public interface ITokensService
{
    Task<string> GenerateAuthToken(UserModel userModel);
    Task<string> GenerateTwoFactorToken(UserModel userModel, string encryptedCode);
    Task<string> GetTokenEmbeddedData(string token, TokenEmbeddedData data);
}
