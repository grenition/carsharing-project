using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedFramework.Data.DTO;
using SharedFramework.Data.DTO.DataTypes;
using Users.Application.Services.Abstract;

namespace Users.API.Controllers;

[Authorize]
[Route("api/users/settings")]
[ApiController]
public class UsersSettingsController(
    ITwoFactorService twoFactorService) : ControllerBase
{
    [HttpGet("two-factor")]
    public async Task<IActionResult> IsTwoFactorActive()
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        return Ok(await twoFactorService.IsTwoFactorActive(userId));
    }
    
    [HttpPut("two-factor")]
    public async Task<IActionResult> SetTwoFactorActive([FromBody] BooleanRequest request)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        return Ok(await twoFactorService.SetTwoFactorActive(userId, request));
    }
}
