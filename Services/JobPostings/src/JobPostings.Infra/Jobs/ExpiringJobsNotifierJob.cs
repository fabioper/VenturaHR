using JobPostings.Application.Services.Contracts;
using Microsoft.Extensions.Logging;
using Quartz;

namespace JobPostings.Infra.Jobs;

[DisallowConcurrentExecution]
public class ExpiringJobsNotifierJob : IJob
{
    private readonly IExpiringJobsNotifierService _expiringJobsNotifierService;

    public ExpiringJobsNotifierJob(IExpiringJobsNotifierService expiringJobsNotifierService)
        => _expiringJobsNotifierService = expiringJobsNotifierService;

    public async Task Execute(IJobExecutionContext context)
        => await _expiringJobsNotifierService.NotifyCompaniesOfExpiringJobs();
}