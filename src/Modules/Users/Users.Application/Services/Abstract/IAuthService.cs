using Users.Application.DTO;

namespace Users.Application.Services.Abstract;

public interface IAuthService
{
    Task<UserAuthResponse> AuthenticateAsync(UserAuthDto authDto);
}
