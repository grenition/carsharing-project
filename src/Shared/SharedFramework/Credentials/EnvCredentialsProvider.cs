using DotNetEnv;
using SharedFramework.Credentials.Exceptions;

namespace SharedFramework.Credentials;

public class EnvCredentialsProvider : ICredentialsProvider
{
    private static readonly Dictionary<CredentialType, string> EnvMap = new()
    {
        { CredentialType.JwtKey, "JWT_KEY" }
    };
    
    public EnvCredentialsProvider(string envFilePath)
    {
        Env.Load(envFilePath);
    }
    
    public Task<string> GetAsync(CredentialType type)
    {
        if (!EnvMap.ContainsKey(type))
            throw new EnvCredentialNotMappedException();

        var envKey = EnvMap[type];
        var credential = Environment.GetEnvironmentVariable(envKey) ?? throw new EnvNotFoundException();
        
        return Task.FromResult(credential);
    }
}
