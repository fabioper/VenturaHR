using Microsoft.AspNetCore.Mvc;
using Users.Api.DTOs;
using Users.Api.Services.Contracts;

namespace Users.Api.Controllers;

[ApiController]
[Route("/accounts")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
        => _usersService = usersService;

    [HttpPost("/companies")]
    public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompanyInput dto)
    {
        await _usersService.RegisterCompany(dto);
        return Ok();
    }

    [HttpPost("/applicants")]
    public async Task<IActionResult> RegisterJobApplicant([FromBody] RegisterApplicantInput dto)
    {
        await _usersService.RegisterApplicant(dto);
        return Ok();
    }
}
