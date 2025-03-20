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
    private readonly IEmailService _emailService;
    private readonly UserManager<UserModel> _userManager;
    private readonly ITwoFactorTokenService _twoFactorTokenService;

    public AuthenticationService(
        ITokenFactory tokenFactory,
        IEmailService emailService,
        UserManager<UserModel> userManager,
        ITwoFactorTokenService twoFactorTokenService)
    {
        _tokenFactory = tokenFactory;
        _emailService = emailService;
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
            await _emailService.SendTwoFactorCode(user.Email!, twoFactorCode);

            return new UserAuthResponse(
                userName: user.UserName!,
                token: null!,
                requires2fa: true,
                message: "Two-factor authentication required. A verification code has been sent to email.");
        }

        var token = await _tokenFactory.GenerateAuthToken(user);
        return new UserAuthResponse(
            userName: user.UserName!,
            token: token,
            requires2fa: false,
            message: "Authenticated successfully.");
    }
    public Task<UserAuthResponse> AuthenticateTwoFactor(UserAuthTwoFactorRequest authTwoFactorRequest)
    {
        throw new NotImplementedException();
    }
}
