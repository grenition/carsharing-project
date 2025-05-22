using Microsoft.Extensions.Configuration;
using SharedFramework.Credentials.Exceptions;

namespace SharedFramework.Credentials;

public sealed class AppSettingsCredentialsProvider : ICredentialsProvider
{
    private readonly IConfiguration _section;

    private static readonly Dictionary<CredentialType, string> ConfigMap = new()
    {
        { CredentialType.JwtKey,           "JwtKey" },
        { CredentialType.TwoFactorKey,     "TwoFactorKey" },
        { CredentialType.SmtpUserAddress,  "SmtpUserAddress" },
        { CredentialType.SmtpUserPassword, "SmtpUserPassword" }
    };

    public AppSettingsCredentialsProvider(IConfiguration configuration)
    {
        _section = configuration.GetSection("Credentials");
    }

    public Task<string> GetAsync(CredentialType type)
    {
        if (!ConfigMap.TryGetValue(type, out var key))
            throw new CredentialNotMappedException();

        var value = _section[key];
        if (string.IsNullOrWhiteSpace(value))
            throw new CredentialNotFoundException();

        return Task.FromResult(value);
    }
}