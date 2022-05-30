using JobPostings.Api.Common.Constants;
using JobPostings.Application.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPostings.Api.Controllers;

[ApiController]
[Route("applications")]
[Authorize(Policy = Policy.ApplicantOnly)]
public class ApplicationsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetApplications() => Ok();

    [HttpPost]
    public async Task<IActionResult> ApplyToJobPosting([FromBody] ApplyToJobRequest request) => Ok();
}