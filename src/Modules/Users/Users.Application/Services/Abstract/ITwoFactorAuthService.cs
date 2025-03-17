namespace Users.Application.Services.Abstract;

public interface ITwoFactorAuthService
{
    // TODO: refactor functions parameters to Dto
    
    Task EnableTwoFactorAsync(string userId);
    Task DisableTwoFactorAsync(string userId);
    Task RequestTwoFactorCodeAsync(string userId);
    Task ConfirmTwoFactorCodeAsync(string userId, string code);
}
