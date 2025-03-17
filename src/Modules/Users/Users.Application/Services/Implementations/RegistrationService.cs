using Microsoft.AspNetCore.Identity;
using Users.Application.DTO;
using Users.Application.Exception;
using Users.Application.Services.Abstract;
using Users.Domain.Models;

namespace Users.Application.Services.Implementations;

public class RegistrationService : IRegistrationService
{
    private readonly UserManager<UserModel> _userManager;
    private readonly IEmailService _emailService;

    public RegistrationService(UserManager<UserModel> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }
    
    public async Task RegisterAsync(UserRegisterDto registerDto)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email!);
        if (existingUser != null)
            throw new UserAlreadyExistsException();

        var user = new UserModel()
        {
            UserName = registerDto.Username,
            Email = registerDto.Email
        };
        
        var createResult = await _userManager.CreateAsync(user, registerDto.Password!);
        if (!createResult.Succeeded)
        {
            var errors = string.Join("; ", createResult.Errors.Select(e => e.Description));
            throw new UserCreationFailedException($"Unable to create user. Errors: {errors}");
        }

        var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        await _emailService.SendVerificationEmailAsync(registerDto.Email!, emailConfirmationToken);
    }
    
    public async Task ConfirmRegistration(UserConfirmRegistrationDto resetPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email!);
        if (user == null)
            throw new UserNotFoundException();

        var confirmResult = await _userManager.ConfirmEmailAsync(user, resetPasswordDto.Token!);
        if (!confirmResult.Succeeded)
            throw new EmailConfirmationException(string.Join("; ", confirmResult.Errors.Select(e => e.Description)));
    }
}
