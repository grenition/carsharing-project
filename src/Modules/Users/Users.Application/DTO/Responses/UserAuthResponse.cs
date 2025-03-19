using SharedFramework.Data.DTO;

namespace Users.Application.DTO.Responses;

public class UserAuthResponse(string userId, string token, string message) : ApiResponse(message)
{
    private string? UserId { get; set; } = userId;
    private string? Token { get; set; } = token;
}

