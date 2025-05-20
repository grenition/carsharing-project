namespace Users.Application.Services.Abstract;

public interface ITwoFactorNumericCodesService
{
    string GenerateNumericCode();
    string EncryptCode(string code);
    string DecryptCode(string encrypted);
}
