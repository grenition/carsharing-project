using Microsoft.AspNetCore.Identity;
using SharedFramework.Extensions;
using Users.Application.DTO.Requests;
using Users.Application.DTO.Responses;
using Users.Application.Exception;
using Users.Application.Factories.Abstract;
using Users.Application.Services.Abstract;
using Users.Domain.Models;

namespace Users.Application.Services;

public class RegistrationService : IRegistrationService
{
    private readonly UserManager<UserModel> _userManager;
    private readonly IEmailService _emailService;
    private readonly IUsernameFactory _usernameFactory;

    public RegistrationService(
        UserManager<UserModel> userManager,
        IEmailService emailService,
        IUsernameFactory usernameFactory)
    {
        _userManager = userManager;
        _emailService = emailService;
        _usernameFactory = usernameFactory;
    }
    
    public async Task<UserResponse> Register(UserRegisterRequest registerRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email!);
        if (existingUser != null)
            throw new UserAlreadyExistsException();

        if (!registerRequest.Email!.IsValidEmailForm())
            throw new InvalidEmailException();
        
        var user = new UserModel()
        {
            UserName = await _usernameFactory.GenerateUniqueUsername(),
            Email = registerRequest.Email
        };
        
        var createResult = await _userManager.CreateAsync(user, registerRequest.Password!);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(" ", createResult.Errors.Select(e => e.Description));
            throw new UserCreationFailureException(errors);
        }

        var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        await _emailService.SendVerificationEmail(registerRequest.Email!, emailConfirmationToken);

        return new UserResponse(user.Id, "User successfully registered. Verification code sended to email.");
    }
    
    public async Task<UserResponse> ConfirmEmail(UserConfirmRegistrationRequest resetPasswordRequest)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordRequest.Email!);
        if (user == null)
            throw new UserNotFoundException();

        var confirmResult = await _userManager.ConfirmEmailAsync(user, resetPasswordRequest.Token!);
        if (!confirmResult.Succeeded)
            throw new EmailConfirmationFailureException(string.Join("; ", confirmResult.Errors.Select(e => e.Description)));
        
        return new UserResponse(user.Id, "User email successfully confirmed.");
    }
}
