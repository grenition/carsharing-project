using Users.Application.DTO.Requests;
using Users.Application.DTO.Responses;

namespace Users.Application.Services.Abstract;

public interface IRegistrationService
{
    Task<UserResponse> Register(UserRegisterRequest registerRequest);
    Task<UserResponse> ConfirmEmail(UserConfirmRegistrationRequest resetPasswordRequest);
}
