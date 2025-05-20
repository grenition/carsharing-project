using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedFramework.Data.DTO.DataTypes;
using Users.Application.Services.Abstract;

namespace Users.API.Controllers;

[Authorize]
[Route("api/users/settings")]
[ApiController]
public class UsersSettingsController(
    ITwoFactorSettingsService twoFactorSettingsService) : ControllerBase
{
    [HttpGet("two-factor")]
    public async Task<IActionResult> IsTwoFactorActive()
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        return Ok(await twoFactorSettingsService.IsTwoFactorActive(userId));
    }
    
    [HttpPut("two-factor")]
    public async Task<IActionResult> SetTwoFactorActive([FromBody] BooleanRequest request)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        return Ok(await twoFactorSettingsService.SetTwoFactorActive(userId, request));
    }
}
