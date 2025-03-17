using Microsoft.AspNetCore.Mvc;
using SharedFramework.Exceptions;

namespace AppHost;

[Route("api/test")]
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
        throw new TestException("Test exception");
        return Ok(new TestResultDto("Test success!"));
    }
}

public class TestException : ApiException
{
    public TestException(string? message) : base(message)
    {
    }
}
