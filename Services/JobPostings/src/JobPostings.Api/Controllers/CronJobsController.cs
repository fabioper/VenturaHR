using JobPostings.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JobPostings.Api.Controllers;

[ApiController]
[Route("cron-jobs")]
public class CronJobsController : ControllerBase
{
    private readonly IExpiringJobsNotifierService _expiringJobsNotifier;

    public CronJobsController(IExpiringJobsNotifierService expiringJobsNotifier)
        => _expiringJobsNotifier = expiringJobsNotifier;

    [HttpPost("jobs-about-to-expire")]
    public async Task<IActionResult> NotifyCompaniesAboutJobsAboutToExpire()
    {
        await _expiringJobsNotifier.NotifyCompaniesOfExpiringJobs();
        return Ok();
    }
}