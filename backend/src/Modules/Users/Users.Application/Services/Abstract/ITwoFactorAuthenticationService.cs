using Users.Application.DTO.Requests;
using Users.Application.DTO.Responses;
using Users.Domain.Models;

namespace Users.Application.Services.Abstract;

public interface ITwoFactorAuthenticationService
{
    internal Task<UserAuthResponse> GenerateTokenAndSendCode(UserModel user);
    Task<UserAuthResponse> Authenticate(UserTwoFactorAuthRequest authRequest);
}
