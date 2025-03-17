using Users.Application.DTO;
using Users.Application.Services.Abstract;

namespace Users.Application.Services.Implementations;

public class AuthService : IAuthService
{
    public Task<UserAuthResponse> AuthenticateAsync(UserAuthDto authDto)
    {
        throw new NotImplementedException();
    }
}
