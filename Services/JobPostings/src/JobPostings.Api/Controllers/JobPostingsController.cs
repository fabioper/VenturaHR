using JobPostings.Api.Constants;
using JobPostings.Api.Extensions.Principal;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPostings.Api.Controllers;

[ApiController]
[Route("job-postings")]
public class JobPostingsController : ControllerBase
{
    private readonly IJobPostingsService _jobPostingsService;

    public JobPostingsController(IJobPostingsService jobPostingsService)
        => _jobPostingsService = jobPostingsService;

    [HttpPost]
    [Authorize(Policy = Policy.CompanyOnly)]
    public async Task<IActionResult> PostJob([FromBody] PostJobRequest request)
    {
        await _jobPostingsService.PublishJob(request with
        {
            CompanyId = User.GetId(),
        });

        return Ok();
    }

    [HttpGet]
    [Authorize(Policy = Policy.CompanyOnly)]
    public async Task<IActionResult> GetCompanyPublishedJobs()
    {
        var jobPostings = await _jobPostingsService.GetPublishedJobsBy(User.GetId());
        return Ok(jobPostings);
    }
}