namespace Users.Application.Factories.Abstract;

public interface IUsernameFactory
{
    Task<string> GenerateUniqueUsername();
}
