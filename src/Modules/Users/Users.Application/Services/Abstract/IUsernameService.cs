namespace Users.Application.Services.Abstract;

public interface IUsernameService
{
    Task<bool> IsUsernameTaken(string username);
    Task<string> GenerateUniqueUsername();
}
