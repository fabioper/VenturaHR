using JobPostings.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JobPostings.Api.Controllers;

[ApiController]
[Route("jobs")]
public class JobsController : ControllerBase
{
    private readonly IJobPostingsService _jobPostingsService;

    public JobsController(IJobPostingsService jobPostingsService)
    {
        _jobPostingsService = jobPostingsService;
    }

    [HttpPost("jobs-about-to-expire")]
    public async Task<IActionResult> NotifyCompaniesAboutJobsAboutToExpire()
    {
        await _jobPostingsService.NotifyCompaniesOfJobsAboutToExpire(); 
        return Ok();
    }
}