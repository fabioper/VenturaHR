using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Commands;
using Users.Application.Models.Requests;

namespace Users.Api.Controllers;

[ApiController, Route("/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> FinishUserProfile([FromBody] FinishUserProfileDto dto)
    {
        await _mediator.Send(new FinishUserProfileCommand(dto.ExternalId, dto.Name, dto.Email, dto.Role));
        return Ok();
    }
}