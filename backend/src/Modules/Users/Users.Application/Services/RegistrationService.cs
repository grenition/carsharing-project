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
    private readonly IUsernameFactory _usernameFactory;

    public RegistrationService(
        UserManager<UserModel> userManager,
        IUsernameFactory usernameFactory)
    {
        _userManager = userManager;
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

        return new UserResponse(user.Id, "User successfully registered.");
    }
}
