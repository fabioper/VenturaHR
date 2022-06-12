using JobPostings.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JobPostings.Api.Controllers;

[ApiController]
[Route("jobs")]
public class JobsController : ControllerBase
{
    private readonly IExpiringJobsNotifierService _expiringJobsNotifier;

    public JobsController(IExpiringJobsNotifierService expiringJobsNotifier)
        => _expiringJobsNotifier = expiringJobsNotifier;

    [HttpPost("jobs-about-to-expire")]
    public async Task<IActionResult> NotifyCompaniesAboutJobsAboutToExpire()
    {
        await _expiringJobsNotifier.NotifyCompaniesOfExpiringJobs();
        return Ok();
    }
}