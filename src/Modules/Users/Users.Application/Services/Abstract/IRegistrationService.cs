using Users.Application.DTO;

namespace Users.Application.Services.Abstract;

public interface IRegistrationService
{
    Task RegisterAsync(UserRegisterDto registerDto);
    Task ConfirmRegistration(UserConfirmRegistrationDto resetPasswordDto);
}
