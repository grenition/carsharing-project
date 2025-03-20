using SharedFramework.Data.DTO;

namespace Users.Application.DTO.Responses;

public class UserAuthResponse(string userName, string token, bool requires2fa, string message) : ApiResponse(message)
{
    public string? UserName { get; set; } = userName;
    public string? Token { get; set; } = token;
    public bool RequiresTwoFactor { get; set; } = requires2fa;
}

