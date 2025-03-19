using Microsoft.AspNetCore.Mvc;
using Users.Application.DTO.Requests;
using Users.Application.Services.Abstract;

namespace Users.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(
    IAuthService authService,
    IRegistrationService registrationService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserAuthRequest userAuthRequest)
    {
        return Ok(await authService.AuthenticateAsync(userAuthRequest));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
    {
        return Ok(await registrationService.RegisterAsync(userRegisterRequest));
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] UserConfirmRegistrationRequest registrationRequest)
    {
        return Ok(await registrationService.ConfirmEmail(registrationRequest));
    }
    
}
