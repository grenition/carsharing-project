namespace Users.Application.DTO;

public record UserAuthResponse
{
    private string? UserId { get; set; }
    private string? Token { get; set; }
}

