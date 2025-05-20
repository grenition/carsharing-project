using SharedFramework.Data.DTO;

namespace Users.Application.DTO.Responses;

public class UserResponse(string userId, string message) : ApiResponse(message)
{
    public string? UserId { get; set; } = userId;
}
