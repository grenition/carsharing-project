using Microsoft.AspNetCore.Mvc;
using Users.Application.DTO.Requests;
using Users.Application.Services.Abstract;

namespace Users.API.Controllers;

[Route("api/users/auth")]
[ApiController]
public class UsersAuthController(
    IAuthenticationService authenticationService,
    IRegistrationService registrationService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserAuthRequest userAuthRequest)
    {
        return Ok(await authenticationService.Authenticate(userAuthRequest));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
    {
        return Ok(await registrationService.Register(userRegisterRequest));
    }
}
