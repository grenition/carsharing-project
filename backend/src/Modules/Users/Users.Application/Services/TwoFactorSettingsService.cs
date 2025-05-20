using Microsoft.AspNetCore.Identity;
using SharedFramework.Data.DTO.DataTypes;
using Users.Application.DTO.Responses;
using Users.Application.Exception;
using Users.Application.Services.Abstract;
using Users.Domain.Models;

namespace Users.Application.Services;

public class TwoFactorSettingsService : ITwoFactorSettingsService
{
    private readonly UserManager<UserModel> _userManager;

    public TwoFactorSettingsService(UserManager<UserModel> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<UserTwoFactorStatusResponse> IsTwoFactorActive(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundException();

        return new UserTwoFactorStatusResponse
        (
            twoFactorEnabled: user.TwoFactorEnabled,
            userId: user.Id,
            message: $"Two factor authentication state: {user.TwoFactorEnabled}."
        );
    }
    
    public async Task<UserTwoFactorStatusResponse> SetTwoFactorActive(string userId, BooleanRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundException();
        
        if (!user.EmailConfirmed && request.State)
            throw new EmailNotConfirmedException("Unable to activate two factor, please confirm email.");

        await _userManager.SetTwoFactorEnabledAsync(user, request.State);

        return new UserTwoFactorStatusResponse
        (
            twoFactorEnabled: request.State,
            userId: user.Id,
            message: "Two-factor authentication enabled successfully."
        );
    }
}
