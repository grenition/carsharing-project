using Users.Application.DTO.Requests;
using Users.Application.DTO.Responses;

namespace Users.Application.Services.Abstract;

public interface IAuthenticationService
{
    Task<UserAuthResponse> AuthenticateAsync(UserAuthRequest authRequest);
    Task<UserAuthResponse> AuthenticateTwoFactor(UserAuthTwoFactorRequest authTwoFactorRequest);
}
