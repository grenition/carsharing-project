namespace Users.Application.DTO.Requests;

public class UserTwoFactorAuthRequest
{
    public string? Token { get; set; }
    public string? Code { get; set; }
}
