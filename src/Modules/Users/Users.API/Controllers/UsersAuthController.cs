using Microsoft.AspNetCore.Mvc;
using Users.Application.DTO.Requests;
using Users.Application.Services.Abstract;

namespace Users.API.Controllers;

[Route("api/users/auth")]
[ApiController]
public class UsersAuthController(
    IAuthenticationService authenticationService,
    IRegistrationService registrationService,
    ITwoFactorAuthenticationService twoFactorAuthenticationService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserAuthRequest userAuthRequest)
    {
        return Ok(await authenticationService.Authenticate(userAuthRequest));
    }
    
    [HttpPost("login/two-factor")]
    public async Task<IActionResult> Login2Fa([FromBody] UserTwoFactorAuthRequest userAuthRequest)
    {
        return Ok(await twoFactorAuthenticationService.Authenticate(userAuthRequest));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
    {
        return Ok(await registrationService.Register(userRegisterRequest));
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] UserConfirmRegistrationRequest registrationRequest)
    {
        return Ok(await registrationService.ConfirmEmail(registrationRequest));
    }
}
