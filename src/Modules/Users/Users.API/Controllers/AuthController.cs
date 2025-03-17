using Microsoft.AspNetCore.Mvc;
using Users.Application.DTO;
using Users.Application.Services.Abstract;

namespace Users.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(
    IAuthService authService,
    IRegistrationService registrationService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserAuthDto userAuthDto)
    {
        return Ok(await authService.AuthenticateAsync(userAuthDto));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        await registrationService.RegisterAsync(userRegisterDto);
        return Ok();
    }

    [HttpPost("confitmRegistration")]
    public async Task<IActionResult> ConfirmRegistration([FromBody] UserConfirmRegistrationDto registrationDto)
    {
        await registrationService.ConfirmRegistration(registrationDto);
        return Ok();
    }
    
}
