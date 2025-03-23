using SharedFramework.Data.DTO.DataTypes;
using Users.Application.DTO.Responses;

namespace Users.Application.Services.Abstract;

public interface ITwoFactorSettingsService
{
    Task<UserTwoFactorStatusResponse> IsTwoFactorActive(string userId);
    Task<UserTwoFactorStatusResponse> SetTwoFactorActive(string userId, BooleanRequest request);
}
