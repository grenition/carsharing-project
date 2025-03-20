namespace Users.Application.Services.Abstract;

public interface ITwoFactorTokenService
{
    Task<string> GenerateTwoFactorCode();
    Task<bool> IsValidToken(string code);
}
