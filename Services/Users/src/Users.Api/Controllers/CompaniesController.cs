using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Commands;
using Users.Application.Models.Requests;

namespace Users.Api.Controllers;

[ApiController]
[Route("/companies")]
public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyProfileRequest request)
    {
        var command = new CreateCompanyCommand
        {
            Email = request.Email,
            Identifier = request.Identifier,
            Name = request.Email,
            Registration = request.Registration,
            PhoneNumber = request.PhoneNumber,
        };

        await _mediator.Send(command);
        return Ok();
    }
}