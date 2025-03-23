using Microsoft.Extensions.Options;
using SharedFramework.Authentication.Configs;
using Users.Application.Services.Abstract;

namespace Users.Infrastructure.Services;

public class TwoFactorNumericCodesService : ITwoFactorNumericCodesService
{
    private readonly INumericCodesService _numericCodesService;
    private readonly TwoFactorConfig _twoFactorConfig;

    public TwoFactorNumericCodesService(
        INumericCodesService numericCodesService,
        IOptions<TwoFactorConfig> twoFactorConfig)
    {
        _numericCodesService = numericCodesService;
        _twoFactorConfig = twoFactorConfig.Value;
    }
    
    public string GenerateNumericCode() =>
        _numericCodesService.GenerateNumericCode(_twoFactorConfig.CodeLength);

    public string EncryptCode(string code) =>
        _numericCodesService.EncryptCode(code, _twoFactorConfig.Secret!);
    
    public string DecryptCode(string encrypted) =>
        _numericCodesService.DecryptCode(encrypted, _twoFactorConfig.Secret!);
}
