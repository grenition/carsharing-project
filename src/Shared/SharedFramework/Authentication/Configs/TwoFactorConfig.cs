namespace SharedFramework.Authentication.Configs;

public class TwoFactorConfig
{
    public string? Secret { get; set; }
    public int CodeLength { get; set; }
    public int ExpirationMinutes { get; set; }
}
