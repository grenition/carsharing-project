using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Users.Application.Services.Abstract;
using Users.Domain.Models;

namespace Users.Application.Services;

public class UsernameService : IUsernameService
{
    private readonly UserManager<UserModel> _userManager;

    public UsernameService(UserManager<UserModel> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> IsUsernameTaken(string username) =>
        await _userManager.FindByNameAsync(username) != null;
    
    public async Task<string> GenerateUniqueUsername()
    {
        var userCount = await _userManager.Users.CountAsync();
        string username;

        do
        {
            username = $"user{userCount + 1}";
            userCount++;
        }
        while (await IsUsernameTaken(username));

        return username;
    }
}
