namespace SharedFramework.Email;

public class EmailContent(string subject, string Body)
{
    public string? Subject { get; set; } = subject;
    public string? Body { get; set; } = Body;
}
