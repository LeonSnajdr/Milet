using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Milet.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    
    private ILogger<TestController> Logger { get; }
    public TestController(ILogger<TestController> logger)
    {
        Logger = logger;
    }
    
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Test()
    {
        return Ok(true);
    }
    
}