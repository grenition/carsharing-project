using SharedFramework.Data.DTO;
using SharedFramework.Data.DTO.DataTypes;
using Users.Application.DTO.Requests;
using Users.Application.DTO.Responses;

namespace Users.Application.Services.Abstract;

public interface ITwoFactorService
{
    Task<UserTwoFactorResponse> IsTwoFactorActive(string userId);
    Task<UserTwoFactorResponse> SetTwoFactorActive(string userId, BooleanRequest request);
}
