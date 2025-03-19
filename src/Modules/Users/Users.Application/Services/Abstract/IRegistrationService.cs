using Users.Application.DTO.Requests;
using Users.Application.DTO.Responses;

namespace Users.Application.Services.Abstract;

public interface IRegistrationService
{
    Task<UserRegisterResponse> RegisterAsync(UserRegisterRequest registerRequest);
    Task<UserRegisterResponse> ConfirmEmail(UserConfirmRegistrationRequest resetPasswordRequest);
}
