namespace Users.Application.Services.Abstract;

public interface INumericCodesService
{
    string GenerateNumericCode(int length);
    string EncryptCode(string code, string secret);
    string DecryptCode(string encrypted, string secret);
}
