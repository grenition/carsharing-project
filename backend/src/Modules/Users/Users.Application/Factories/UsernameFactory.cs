using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Users.Application.Factories.Abstract;
using Users.Domain.Models;

namespace Users.Application.Factories;

public class UsernameFactory : IUsernameFactory
{
    private readonly UserManager<UserModel> _userManager;

    public UsernameFactory(UserManager<UserModel> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<string> GenerateUniqueUsername()
    {
        var userCount = await _userManager.Users.CountAsync();
        string username;

        do
        {
            username = $"user{userCount + 1}";
            userCount++;
        }
        while (await _userManager.FindByNameAsync(username) != null);

        return username;
    }
}
