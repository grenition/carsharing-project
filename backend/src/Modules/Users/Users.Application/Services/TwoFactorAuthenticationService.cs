using Microsoft.AspNetCore.Identity;
using Users.Application.DTO.Requests;
using Users.Application.DTO.Responses;
using Users.Application.Exception;
using Users.Application.Services.Abstract;
using Users.Domain.Models;

namespace Users.Application.Services;

public class TwoFactorAuthenticationService : ITwoFactorAuthenticationService
{
    private readonly ITokensSendingService _tokensSendingService;
    private readonly ITokensService _tokensService;
    private readonly ITwoFactorNumericCodesService _twoFactorNumericCodesService;
    private readonly UserManager<UserModel> _userManager;

    public TwoFactorAuthenticationService(
        ITokensSendingService tokensSendingService, 
        ITokensService tokensService,
        ITwoFactorNumericCodesService twoFactorNumericCodesService,
        UserManager<UserModel> userManager)
    {
        _tokensSendingService = tokensSendingService;
        _tokensService = tokensService;
        _twoFactorNumericCodesService = twoFactorNumericCodesService;
        _userManager = userManager;
    }
    
    public async Task<UserAuthResponse> GenerateTokenAndSendCode(UserModel user)
    {
        if (!user.TwoFactorEnabled)
            throw new UserInvalidConfigurationException("Two factor authentication is disabled");

        var numericCode = _twoFactorNumericCodesService.GenerateNumericCode();
        var encryptedNumericCode = _twoFactorNumericCodesService.EncryptCode(numericCode);
        var token = await _tokensService.GenerateTwoFactorToken(user, encryptedNumericCode);
        await _tokensSendingService.SendTwoFactorCode(user.Email!, numericCode);
        
        return new UserAuthResponse(
            userId: user.Id,
            token: token,
            requires2fa: true,
            message: "Please complete two factor verification. Code has been sended to email.");
    }
    
    public async Task<UserAuthResponse> Authenticate(UserTwoFactorAuthRequest authRequest)
    {
        var email = await _tokensService.GetTokenEmbeddedData(authRequest.Token!, TokenEmbeddedData.UserEmail);
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            throw new UserNotFoundException();

        if (!user.TwoFactorEnabled)
            throw new UserInvalidConfigurationException("Two factor authentication is disabled");

        var encryptedCode = await _tokensService.GetTokenEmbeddedData(authRequest.Token!, TokenEmbeddedData.TwoFactorCode);
        var decryptedCode = _twoFactorNumericCodesService.DecryptCode(encryptedCode);
        var code = authRequest.Code;

        if (code != decryptedCode)
            throw new NumericCodeIsInvalidException();
        
        var token = await _tokensService.GenerateAuthToken(user);
        return new UserAuthResponse(
            userId: user.Id,
            token: token,
            requires2fa: false,
            message: "Authenticated successfully.");
    }
}
