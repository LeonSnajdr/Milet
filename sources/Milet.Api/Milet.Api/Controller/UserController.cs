using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Milet.Api.Contracts;
using Milet.Api.Services;
using Milet.Api.Models;

namespace Milet.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private ILogger<AuthController> Logger { get; }

    private IUserService Service { get; }

    public UserController(ILogger<AuthController> logger, IUserService service)
    {
        Logger = logger;
        Service = service;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetUser()
    {
        var userId = User.FindFirstValue("Id");

        var user = await Service.GetById(userId);

        if (user == null)
        {
            ModelState.AddModelError(nameof(user), ErrorCodes.NotFound.ToString());
            return ValidationProblem(ModelState);
        }
        
        return Ok(user);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserContract createUser)
    {

        var userExists = await Service.Exists(createUser.Username);

        if (userExists)
        {
            ModelState.AddModelError(nameof(createUser.Username), ErrorCodes.AlreadyExists.ToString());
            return ValidationProblem(ModelState);
        }
        
        var user = await Service.Create(createUser);

        return Ok(user);
    }
}