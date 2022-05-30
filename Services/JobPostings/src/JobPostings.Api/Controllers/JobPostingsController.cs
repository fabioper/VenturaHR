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
        var jobPostings = await _jobPostingsService.GetPublishedJobsBy(User.GetId());
        return Ok(jobPostings);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetJobPosting([FromRoute] Guid id) => Ok();

    [HttpPost]
    public async Task<IActionResult> CreateJobPosting([FromBody] CreateJobPostingRequest postingRequest)
    {
        await _jobPostingsService.PublishJob(postingRequest with
        {
            CompanyId = User.GetId(),
        });

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateJobPosting([FromBody] UpdateJobRequest request, [FromRoute] Guid id)
    {
        return Ok();
    }

    [HttpGet("{id:guid}/applications")]
    public async Task<IActionResult> GetJobPostingApplications([FromRoute] Guid id)
    {
        return Ok();
    }
}