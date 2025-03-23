using System.Security.Cryptography;
using System.Text;
using Users.Application.Services.Abstract;

namespace Users.Infrastructure.Services;

public class NumericCodesService : INumericCodesService
{
    public string GenerateNumericCode(int length)
    {
        var rng = new Random();
        var code = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            code.Append(rng.Next(0, 10));
        }
        return code.ToString();
    }

    public string EncryptCode(string code, string secret)
    {
        var key = GetAesKeyFromSecret(secret);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        var plainBytes = Encoding.UTF8.GetBytes(code);
        var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        var resultBytes = aes.IV.Concat(cipherBytes).ToArray();
        return Convert.ToBase64String(resultBytes);
    }

    public string DecryptCode(string encrypted, string secret)
    {
        var key = GetAesKeyFromSecret(secret);
        var fullCipher = Convert.FromBase64String(encrypted);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = fullCipher.Take(16).ToArray();
        var cipherBytes = fullCipher.Skip(16).ToArray();

        using var decryptor = aes.CreateDecryptor();
        var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
        return Encoding.UTF8.GetString(plainBytes);
    }

    private byte[] GetAesKeyFromSecret(string secret)
    {
        using var sha = SHA256.Create();
        return sha.ComputeHash(Encoding.UTF8.GetBytes(secret));
    }
}
