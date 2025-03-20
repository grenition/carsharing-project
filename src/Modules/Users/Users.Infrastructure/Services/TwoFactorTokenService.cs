using Users.Application.Services.Abstract;

namespace Users.Infrastructure.Services;

public class TwoFactorTokenService : ITwoFactorTokenService
{
    public Task<string> GenerateTwoFactorCode()
    {
        throw new NotImplementedException();
    }
    public Task<bool> IsValidToken(string code)
    {
        throw new NotImplementedException();
    }
}
