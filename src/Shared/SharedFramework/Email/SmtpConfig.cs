namespace SharedFramework.Email;

public class SmtpConfig
{
    public string? ServerAddress { get; set; }
    public int ServerPort { get; set; }
    public string? UserAddress { get; set; }
    public string? UserPassword { get; set; }
    public string? SecurityOption { get; set; } 
}
