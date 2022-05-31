using JobPostings.Api.Common.Constants;
using JobPostings.Api.Common.Extensions;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPostings.Api.Controllers;

[ApiController]
[Route("job-postings")]
[Authorize(Policy = Policy.CompanyOnly)]
public class JobPostingsController : ControllerBase
{
    private readonly IJobPostingsService _jobPostingsService;

    public JobPostingsController(IJobPostingsService jobPostingsService)
        => _jobPostingsService = jobPostingsService;

    [HttpGet]
    public async Task<IActionResult> GetCompanyPublishedJobs()
    {
        var companyId = User.GetId();
        var jobPostings = await _jobPostingsService.GetPublishedJobsBy(companyId);
        return Ok(jobPostings);
    }

    [HttpGet("{jobPostingId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetJobPosting([FromRoute] Guid jobPostingId)
    {
        var jobPosting = await _jobPostingsService.GetJobPostingOfId(jobPostingId);
        return Ok(jobPosting);
    }

    [HttpGet("{jobPostingId:guid}/applications")]
    public async Task<IActionResult> GetJobPostingApplications([FromRoute] Guid jobPostingId)
    {
        var applications = await _jobPostingsService.GetJobPostingApplications(jobPostingId);
        return Ok(applications);
    }

    [HttpPost]
    public async Task<IActionResult> CreateJobPosting([FromBody] CreateJobPostingRequest request)
    {
        var companyId = User.GetId();
        await _jobPostingsService.CreateJobPosting(companyId, request);
        return Ok();
    }

    [HttpPut("{jobPostingId:guid}")]
    public async Task<IActionResult> UpdateJobPosting(
        [FromBody] UpdateJobRequest request,
        [FromRoute] Guid jobPostingId)
    {
        await _jobPostingsService.UpdateJob(jobPostingId, request);
        return Ok();
    }
}