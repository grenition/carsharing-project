using Users.Domain.Models;

namespace Users.Application.Factories.Abstract;

public interface ITokensFactory
{
    Task<string> GenerateAuthToken(UserModel userModel);
    Task<string> GenerateTwoFactorToken(UserModel userModel, string encryptedCode);
}
