using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Commands;
using Users.Application.Models.Requests;

namespace Users.Api.Controllers;

[ApiController]
[Route("/applicants")]
public class ApplicantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApplicantsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateApplicant([FromBody] CreateApplicantProfileRequest request)
    {
        var command = new CreateApplicantCommand
        {
            Name = request.Name,
            Email = request.Email,
            Identifier = request.Identifier,
        };
        await _mediator.Send(command);
        return Ok();
    }
}