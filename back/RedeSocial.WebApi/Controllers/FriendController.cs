using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Application.Contracts;
using RedeSocial.Application.Contracts.Documents.Request;

namespace RedeSocial.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FriendController : ControllerBase
{
    private readonly IFriendService _friendService;

    public FriendController(IFriendService friendService)
    {
        _friendService = friendService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _friendService.GetAll(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value!);
    
        return Ok(response);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] FriendRequest friendRequest)
    {
        var response = await _friendService.AddFriend(friendRequest, User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value!);
        if (!response.IsValid)
        {
            return BadRequest(response.Notifications);
        }
        return Ok(response.Success);
    }

    [HttpGet("requests")]
    public async Task<IActionResult> GetRequests()
    {
        var requests = await _friendService.GetFriendRequests(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value!);

        return Ok(requests);
    }
    
    [HttpPost("accept")]
    public async Task<IActionResult> AcceptFriend([FromBody] AcceptFriendRequest request)
    {
        var response = await _friendService.Accept(request, User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value!);
        
        if (!response.IsValid)
        {
            return BadRequest(response.Notifications);
        }
        return Ok(response.Success);
    }
}
