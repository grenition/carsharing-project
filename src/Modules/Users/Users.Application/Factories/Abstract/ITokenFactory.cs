using Users.Domain.Models;

namespace Users.Application.Factories.Abstract;

public interface ITokenFactory
{
    Task<string> GenerateAuthToken(UserModel userModel);
}
