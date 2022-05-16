using JobPostings.Api.Constants;
using JobPostings.Api.DTOs.Requests;
using JobPostings.Api.Extensions;
using JobPostings.Application.Commands.PostJob;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPostings.Api.Controllers;

[ApiController]
[Route("job-postings")]
public class JobPostingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobPostingsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [Authorize(Policy = Policy.CompanyOnly)]
    public async Task<IActionResult> PostJob([FromBody] PostJobRequest request)
    {
        var command = new PostJobCommand
        {
            Role = request.Role,
            Description = request.Description,
            Location = request.Location,
            Salary = request.Salary,
            ExpirationDate = request.ExpirationDate,
            CompanyId = User.GetId(),
        };

        await _mediator.Send(command);

        return Ok();
    }
}