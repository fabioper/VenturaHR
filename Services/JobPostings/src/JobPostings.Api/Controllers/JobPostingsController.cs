using JobPostings.Api.Common.Constants;
using JobPostings.Api.Common.Extensions;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.Services.Contracts;
using JobPostings.CrossCutting.Filters;
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
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] BaseFilter baseFilter)
    {
        var jobPostings = await _jobPostingsService.GetJobPostings(baseFilter);
        return Ok(jobPostings);
    }

    [HttpGet("{jobPostingId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromRoute] Guid jobPostingId)
    {
        var jobPosting = await _jobPostingsService.GetJobPostingOfId(jobPostingId);
        return Ok(jobPosting);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobPostingRequest request)
    {
        await _jobPostingsService.CreateJobPosting(CompanyId, request);
        return Ok();
    }

    [HttpPut("{jobPostingId:guid}")]
    public async Task<IActionResult> Update([FromBody] UpdateJobRequest request, [FromRoute] Guid jobPostingId)
    {
        await _jobPostingsService.UpdateJob(jobPostingId, request);
        return Ok();
    }

    [HttpGet("{jobPostingId:guid}/applications")]
    public async Task<IActionResult> GetJobPostingApplications([FromRoute] Guid jobPostingId)
    {
        var companyId = User.GetId();
        var applications = await _jobPostingsService.GetJobPostingApplications(companyId, jobPostingId);
        return Ok(applications);
    }
    
    private Guid CompanyId => User.GetId();
}