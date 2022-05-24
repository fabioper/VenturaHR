using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Common.Extensions;
using Users.Application.DTOs.Requests;
using Users.Application.Services.Contracts;

namespace Users.Api.Controllers;

[ApiController]
[Route("/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService) =>
        _userService = userService;

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        await _userService.CreateUser(request);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
    {
        var tokenResponse = await _userService.Authenticate(request);
        return Ok(tokenResponse);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var tokenResponse = await _userService.RefreshToken(request);
        return Ok(tokenResponse);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var currentUser = await _userService.GetUserProfile(User.GetId());
        return Ok(currentUser);
    }
}