namespace Users.Application.DTO.Requests;

public class UserRegisterRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? BaseUrl { get; set; }
}
