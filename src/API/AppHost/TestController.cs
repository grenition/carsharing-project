using Microsoft.AspNetCore.Mvc;

namespace AppHost;

[Route("[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    
    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }
    
    public record TestResultDto(string result);
    
    [HttpGet]
    public async Task<ActionResult<TestResultDto>> Test()
    {
        _logger.LogInformation("Test controller action was performed");
        return Ok(new TestResultDto("Test success!"));
    }
}
