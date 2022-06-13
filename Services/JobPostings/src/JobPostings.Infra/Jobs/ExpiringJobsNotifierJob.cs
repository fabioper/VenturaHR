using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.Services.Contracts;
using Microsoft.Extensions.Logging;
using Quartz;

namespace JobPostings.Infra.Jobs;

[DisallowConcurrentExecution]
public class ExpiringJobsNotifierJob : IJob
{
    private readonly IJobPostingExpirationService _jobPostingExpirationService;

    public ExpiringJobsNotifierJob(IJobPostingExpirationService jobPostingExpirationService)
        => _jobPostingExpirationService = jobPostingExpirationService;

    public async Task Execute(IJobExecutionContext context)
        => await _jobPostingExpirationService.NotifyCompaniesOfExpiringJobs();
}