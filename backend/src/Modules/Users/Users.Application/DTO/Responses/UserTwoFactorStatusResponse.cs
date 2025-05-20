namespace Users.Application.DTO.Responses;

public class UserTwoFactorStatusResponse(bool twoFactorEnabled, string userId, string message)
    : UserResponse(userId, message)
{
    public bool TwoFactorEnabled { get; set; } = twoFactorEnabled;
}
