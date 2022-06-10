using JobPostings.Api.Common.Constants;
using JobPostings.Api.Extensions;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.Services.Contracts;
using JobPostings.CrossCutting.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPostings.Api.Controllers;

[ApiController]
[Route("applications")]
[Authorize(Policy = Policy.ApplicantOnly)]
public class ApplicationsController : ControllerBase
{
    private readonly IJobApplicationService _applicationService;

    public ApplicationsController(IJobApplicationService applicationService)
        => _applicationService = applicationService;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ApplicationsFilter filter)
    {
        var applications = await _applicationService.GetAll(filter);
        return Ok(applications);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] JobApplicationRequest request)
    {
        await _applicationService.Apply(ApplicantId, request);
        return Ok();
    }

    private Guid ApplicantId => User.GetId();
}