namespace SharedFramework.Email;

public interface IEmailSender
{
    public Task Send(EmailContent content, string toAddress);
}