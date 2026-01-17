using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Application.Contracts;

namespace RedeSocial.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LikeController : ControllerBase
{
    private readonly ILikeService _likeService;

    public LikeController(ILikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpPost("{postId}")]
    [Authorize]
    public async Task<IActionResult> LikePost(int postId)
    {
        var response = await _likeService.LikePost(postId, User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value!);
        if (!response.IsValid)
        {
            return BadRequest(response.Notifications);
        }
        return Ok(response.Success);
    }

    [HttpDelete("{postId}")]
    [Authorize]
    public async Task<IActionResult> UnlikePost(int postId)
    {
        var response = await _likeService.UnlikePost(postId, User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value!);
        if (!response.IsValid)
        {
            return BadRequest(response.Notifications);
        }
        return Ok(response.Success);
    }
}
