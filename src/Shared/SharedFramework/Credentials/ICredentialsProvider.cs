namespace SharedFramework.Credentials;

public enum CredentialType
{
    JwtKey
}

public interface ICredentialsProvider
{
    public Task<string> GetAsync(CredentialType type);
}
