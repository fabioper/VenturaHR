using JobPostings.Api.Common.Constants;
using JobPostings.Api.Common.Extensions;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.Services.Contracts;
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
    public async Task<IActionResult> GetApplications()
    {
        var applicantId = User.GetId();
        var applications = await _applicationService.GetApplicationsFrom(applicantId);
        return Ok(applications);
    }

    [HttpPost]
    public async Task<IActionResult> ApplyToJobPosting([FromBody] JobApplicationRequest request)
    {
        await _applicationService.Apply(request);
        return Ok();
    }
}