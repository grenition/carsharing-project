namespace Users.Application.DTO.Requests;

public class UserConfirmRegistrationRequest
{
    public string? Email { get; set; }
    public string? Token { get; set; }
}
