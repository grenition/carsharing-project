namespace Users.Application.DTO.Responses;

public class UserAuthResponse(string userId, string token, bool requires2fa, string message) 
    : UserResponse(userId, message)
{
    public string? Token { get; set; } = token;
    public bool RequiresTwoFactor { get; set; } = requires2fa;
}

