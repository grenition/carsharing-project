namespace Users.Application.DTO.Requests;

public class UserAuthTwoFactorRequest
{
    public string? Email { get; set; }
    public string? TwoFactorCode { get; set; }
}
