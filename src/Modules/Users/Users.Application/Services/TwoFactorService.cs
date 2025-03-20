using Microsoft.AspNetCore.Identity;
using SharedFramework.Data.DTO;
using SharedFramework.Data.DTO.DataTypes;
using Users.Application.DTO.Responses;
using Users.Application.Exception;
using Users.Application.Services.Abstract;
using Users.Domain.Models;

namespace Users.Application.Services;

public class TwoFactorService : ITwoFactorService
{
    private readonly UserManager<UserModel> _userManager;

    public TwoFactorService(UserManager<UserModel> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<UserTwoFactorResponse> IsTwoFactorActive(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundException();

        return new UserTwoFactorResponse
        (
            twoFactorEnabled: user.TwoFactorEnabled,
            userId: user.Id,
            message: $"Two factor authentication state: {user.TwoFactorEnabled}."
        );
    }
    
    public async Task<UserTwoFactorResponse> SetTwoFactorActive(string userId, BooleanRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundException();

        await _userManager.SetTwoFactorEnabledAsync(user, request.State);

        return new UserTwoFactorResponse
        (
            twoFactorEnabled: request.State,
            userId: user.Id,
            message: "Two-factor authentication enabled successfully."
        );
    }
}
