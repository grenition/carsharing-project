using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Controllers;

namespace Users.API.Controllers;

[Route("users")]
public class UsersController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register()
    {
        return Ok("poshel nahui");
    }
}
