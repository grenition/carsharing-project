using Microsoft.AspNetCore.Identity;
using Users.Application.DTO.Requests;
using Users.Application.DTO.Responses;
using Users.Application.Exception;
using Users.Application.Factories.Abstract;
using Users.Application.Services.Abstract;
using Users.Domain.Models;

namespace Users.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenFactory _tokenFactory;
    private readonly IVerificationService _verificationService;
    private readonly UserManager<UserModel> _userManager;
    private readonly ITwoFactorTokenService _twoFactorTokenService;

    public AuthenticationService(
        ITokenFactory tokenFactory,
        IVerificationService verificationService,
        UserManager<UserModel> userManager,
        ITwoFactorTokenService twoFactorTokenService)
    {
        _tokenFactory = tokenFactory;
        _verificationService = verificationService;
        _userManager = userManager;
        _twoFactorTokenService = twoFactorTokenService;
    }
    
    public async Task<UserAuthResponse> AuthenticateAsync(UserAuthRequest authRequest)
    {
        var user = await _userManager.FindByEmailAsync(authRequest.Email!);

        if (user == null || !await _userManager.CheckPasswordAsync(user, authRequest.Password!))
            throw new InvalidCredentialsException("Invalid email or password.");

        if (!user.EmailConfirmed)
            throw new EmailNotConfirmedException("Unable to authenticate user, please confirm email.");

        if (user.TwoFactorEnabled)
        {
            var twoFactorCode = await _twoFactorTokenService.GenerateTwoFactorCode();
            await _verificationService.SendTwoFactorCode(user.Email!, twoFactorCode);

            return new UserAuthResponse(
                userId: user.Id,
                token: null!,
                requires2fa: true,
                message: "Two-factor authentication required. A verification code has been sent to email.");
        }

        var token = await _tokenFactory.GenerateAuthToken(user);
        return new UserAuthResponse(
            userId: user.Id,
            token: token,
            requires2fa: false,
            message: "Authenticated successfully.");
    }
    
    public async Task<UserAuthResponse> AuthenticateTwoFactor(UserAuthTwoFactorRequest authTwoFactorRequest)
    {
        var user = await _userManager.FindByEmailAsync(authTwoFactorRequest.Email!);
        if (user == null)
            throw new UserNotFoundException();

        if (!await _twoFactorTokenService.IsValidToken(authTwoFactorRequest.Token!))
            throw new InvalidTokenException("Two factor code is invalid.");
        
        var token = await _tokenFactory.GenerateAuthToken(user);
        return new UserAuthResponse(
            userId: user.Id,
            token: token,
            requires2fa: false,
            message: "Authenticated successfully.");
    }
}
