using SharedFramework.Data.DTO;

namespace Users.Application.DTO.Responses;

public class UserRegisterResponse(string message, string userName) : ApiResponse(message)
{
    public string? UserName { get; set; } = userName;
}
