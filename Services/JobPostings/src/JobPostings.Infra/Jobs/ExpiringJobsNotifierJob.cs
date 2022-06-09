using JobPostings.Application.Services.Contracts;
using Microsoft.Extensions.Logging;
using Quartz;

namespace JobPostings.Infra.Jobs;

[DisallowConcurrentExecution]
public class ExpiringJobsNotifierJob : IJob
{
    private readonly ILogger<ExpiringJobsNotifierJob> _logger;
    private readonly IExpiringJobsNotifierService _expiringJobsNotifierService;

    public ExpiringJobsNotifierJob(
        ILogger<ExpiringJobsNotifierJob> logger,
        IExpiringJobsNotifierService expiringJobsNotifierService)
    {
        _logger = logger;
        _expiringJobsNotifierService = expiringJobsNotifierService;
    }

    public async Task Execute(IJobExecutionContext context)
        => await _expiringJobsNotifierService.NotifyCompaniesOfExpiringJobs();
}