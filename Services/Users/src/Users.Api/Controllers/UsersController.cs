using Microsoft.AspNetCore.Mvc;
using Users.Api.DTOs.Requests;
using Users.Api.Services.Contracts;

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
}