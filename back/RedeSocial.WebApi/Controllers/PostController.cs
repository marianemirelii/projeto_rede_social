using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Application.Contracts;
using RedeSocial.Application.Contracts.Documents.Request;

namespace RedeSocial.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly ICommentService _commentService;

    public PostController(IPostService postService, ICommentService commentService)
    {
        _postService = postService;
        _commentService = commentService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePost([FromBody] PostCreateRequest request)
    {
        var response = await _postService.CreatePost(request, User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value!);
        if (!response.IsValid)
        {
            return BadRequest(response.Notifications);
        }
        return Ok(response.PostDto);
    }

    [HttpGet("feed")]
    [Authorize]
    public async Task<IActionResult> GetAllPosts()
    {
        var response = await _postService.GetAllPosts(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value!);
        return Ok(response);
    }

    [HttpPost("{postId}/comments")]
    [Authorize]
    public async Task<IActionResult> CommentOnPost(int postId, [FromBody] CommentCreateRequest request)
    {
        var response = await _commentService.CommentOnPost(postId, request, User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value!);
        if (!response.IsValid)
        {
            return BadRequest(response.Notifications);
        }
        return Ok(response.Success);
    }

    [HttpGet("{postId}/comments")]
    [Authorize]
    public async Task<IActionResult> GetCommentsByPostId(int postId)
    {
        var comments = await _commentService.GetCommentsByPostId(postId);
        return Ok(comments);
    }
}
