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
    private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;

    public AuthenticationService(
        ITokensService tokensService,
        UserManager<UserModel> userManager,
        ITwoFactorAuthenticationService twoFactorAuthenticationService)
    {
        _tokensService = tokensService;
        _userManager = userManager;
        _twoFactorAuthenticationService = twoFactorAuthenticationService;
    }

    public async Task<UserAuthResponse> Authenticate(UserAuthRequest authRequest)
    {
        var user = await _userManager.FindByEmailAsync(authRequest.Email!);

        if (user == null || !await _userManager.CheckPasswordAsync(user, authRequest.Password!))
            throw new InvalidCredentialsException("Invalid email or password.");

        if (!user.EmailConfirmed)
            throw new EmailNotConfirmedException("Unable to authenticate user, please confirm email.");

        if (user.TwoFactorEnabled)
            return await _twoFactorAuthenticationService.GenerateTokenAndSendCode(user);

        var token = await _tokensService.GenerateAuthToken(user);
        return new UserAuthResponse(
            userId: user.Id,
            token: token,
            requires2fa: false,
            message: "Authenticated successfully.");
    }
}
