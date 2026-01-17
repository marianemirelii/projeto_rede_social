using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Application.Contracts;
using RedeSocial.Application.Contracts.Documents.Request;
using RedeSocial.Domain.Contracts.Documents.Request;
using RedeSocial.WebApi.Security;

namespace RedeSocial.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly TokenService tokenService;

    public UserController(IUserService userService, TokenService tokenService)
    {
        _userService = userService;
        this.tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var response = await _userService.Login(loginRequest);

        if (!response.IsValid || response.LoginDto is null)
        {
            return BadRequest(response.Notifications);
        }

        var token = tokenService.GenerateToken(response.LoginDto);

        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var response = await _userService.RegisterUser(registerRequest);
        if (!response.IsValid)
        {
            return BadRequest(response.Notifications);
        }

        return Ok(response.Success);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUser()
    {
        var response = await _userService.GetUser(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value!);

        if (!response.IsValid)
        {
            return BadRequest(response.Notifications);
        }

        return Ok(response.User);
    }

    [HttpGet("search")]
    [Authorize]
    public async Task<IActionResult> SearchUsers([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
        var response = await _userService.SearchUsers(search, page, limit);

        return Ok(response);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var response = await _userService.UpdateUser(request, User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value!);
        if (!response.IsValid)
        {
            return BadRequest(response.Notifications);
        }

        return Ok(response.Success);
    }

    [HttpGet("profile/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(int userId)
    {
        var response = await _userService.GetUserById(userId, User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value!);

        if (!response.IsValid)
        {
            return BadRequest(response.Notifications);
        }

        return Ok(response.User);
    }
}