using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Milet.Api.Contracts;
using Milet.Api.Services;

namespace Milet.Api.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class FriendshipController : ControllerBase
{
    private IFriendshipService Service { get; }

    public FriendshipController(IFriendshipService service)
    {
        Service = service;
    }
    
    [HttpPost("request/{userAcceptId}")]
    public async Task<IActionResult> RequestFriendship([FromRoute] string userAcceptId)
    {
        var userId = User.FindFirstValue("Id");
        var friendshipExists = await Service.Exists(userId, userAcceptId);

        if (friendshipExists)
        {
            ModelState.AddModelError("friendship", ErrorCodes.AlreadyExists.ToString());
            return ValidationProblem(ModelState);
        }

        var friendship = await Service.RequestFriendship(userId, userAcceptId);

        return Ok(friendship);
    }

    [HttpPost("accept/{friendshipId}")]
    public async Task<IActionResult> AcceptFriendship([FromRoute] string friendshipId)
    {
        await Service.AcceptFriendship(friendshipId);

        return Ok();
    }
}