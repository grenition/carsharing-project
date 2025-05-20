namespace SharedFramework.Credentials;

public enum CredentialType
{
    JwtKey,
    TwoFactorKey,
    SmtpUserAddress,
    SmtpUserPassword
}

public interface ICredentialsProvider
{
    public Task<string> GetAsync(CredentialType type);
}
