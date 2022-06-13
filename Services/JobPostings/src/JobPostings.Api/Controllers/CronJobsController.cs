using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JobPostings.Api.Controllers;

[ApiController]
[Route("cron-jobs")]
public class CronJobsController : ControllerBase
{
    private readonly IJobPostingExpirationService _jobPostingExpiration;

    public CronJobsController(IJobPostingExpirationService jobPostingExpiration)
        => _jobPostingExpiration = jobPostingExpiration;

    [HttpPost("jobs-about-to-expire")]
    public async Task<IActionResult> NotifyCompaniesAboutJobsAboutToExpire()
    {
        await _jobPostingExpiration.NotifyCompaniesOfExpiringJobs();
        return Ok();
    }
}