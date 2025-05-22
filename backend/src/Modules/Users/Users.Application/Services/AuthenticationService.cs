using Microsoft.AspNetCore.Identity;
using Users.Application.DTO.Requests;
using Users.Application.DTO.Responses;
using Users.Application.Exception;
using Users.Application.Services.Abstract;
using Users.Domain.Models;

namespace Users.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokensService _tokensService;
    private readonly UserManager<UserModel> _userManager;

    public AuthenticationService(
        ITokensService tokensService,
        UserManager<UserModel> userManager)
    {
        _tokensService = tokensService;
        _userManager = userManager;
    }

    public async Task<UserAuthResponse> Authenticate(UserAuthRequest authRequest)
    {
        var user = await _userManager.FindByEmailAsync(authRequest.Email!);

        if (user == null || !await _userManager.CheckPasswordAsync(user, authRequest.Password!))
            throw new InvalidCredentialsException("Invalid email or password.");
        
        var token = await _tokensService.GenerateAuthToken(user);
        return new UserAuthResponse(
            userId: user.Id,
            token: token,
            requires2fa: false,
            message: "Authenticated successfully.");
    }
}
