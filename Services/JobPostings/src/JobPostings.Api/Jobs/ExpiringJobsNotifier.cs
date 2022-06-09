using JobPostings.Application.Services.Contracts;
using Quartz;

namespace JobPostings.Api.Jobs;

[DisallowConcurrentExecution]
public class ExpiringJobsNotifier : IJob
{
    private readonly ILogger<ExpiringJobsNotifier> _logger;
    private readonly IJobPostingsService _jobPostingsService;

    public ExpiringJobsNotifier(ILogger<ExpiringJobsNotifier> logger, IJobPostingsService jobPostingsService)
    {
        _logger = logger;
        _jobPostingsService = jobPostingsService;
    }

    public async Task Execute(IJobExecutionContext context)
        => await _jobPostingsService.NotifyCompaniesOfJobsAboutToExpire();
}