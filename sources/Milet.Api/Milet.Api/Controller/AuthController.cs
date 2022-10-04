using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Milet.Api.Contracts;
using Milet.Api.Services;

namespace Milet.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private ILogger<AuthController> Logger { get; }
    
    private IAuthService Service { get; }

    public AuthController(ILogger<AuthController> logger, IAuthService service)
    {
        Logger = logger;
        Service = service;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginContract userLogin)
    {
        var authResponse = await Service.Login(userLogin);
        
        if (authResponse == null)
        {
            ModelState.AddModelError(nameof(authResponse.User), ErrorCodes.Failed.ToString());
            return ValidationProblem(ModelState);
        }

        return Ok(authResponse);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] TokenContract oldToken)
    {
        var token = await Service.Refresh(oldToken);

        if (token == null)
        {
            ModelState.AddModelError(nameof(oldToken.RefreshToken), ErrorCodes.Failed.ToString());
            return ValidationProblem(ModelState);
        }

        return Ok(token);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("logout")]
    public async Task<IActionResult> Logout()
    {
        var userId = User.FindFirstValue("Id");

        await Service.Logout(userId);

        return Ok();
    }
}