using JobPostings.Api.Constants;
using JobPostings.Api.DTOs.Requests;
using JobPostings.Api.Extensions;
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
    {
        _jobPostingsService = jobPostingsService;
    }

    [HttpPost]
    [Authorize(Policy = Policy.CompanyOnly)]
    public async Task<IActionResult> PostJob([FromBody] PostJobRequest request)
    {
        var dto = new PublishJobRequest
        {
            Role = request.Role,
            Description = request.Description,
            Location = request.Location,
            Salary = request.Salary,
            ExpirationDate = request.ExpirationDate,
            CompanyId = User.GetId(),
        };

        await _jobPostingsService.PublishJob(dto);
        return Ok();
    }

    [HttpGet]
    [Authorize(Policy = Policy.CompanyOnly)]
    public async Task<IActionResult> GetPublishedJobsBy()
    {
        var jobPostings = await _jobPostingsService.GetPublishedJobsBy(User.GetId());
        return Ok(jobPostings);
    }
}