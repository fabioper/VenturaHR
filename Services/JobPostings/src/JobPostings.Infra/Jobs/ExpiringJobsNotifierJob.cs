using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.Services.Contracts;
using Microsoft.Extensions.Logging;
using Quartz;

namespace JobPostings.Infra.Jobs;

[DisallowConcurrentExecution]
public class ExpiringJobsNotifierJob : IJob
{
    private readonly IJobPostingStatusService _jobPostingStatusService;

    public ExpiringJobsNotifierJob(IJobPostingStatusService jobPostingStatusService)
        => _jobPostingStatusService = jobPostingStatusService;

    public async Task Execute(IJobExecutionContext context)
        => await _jobPostingStatusService.NotifyCompaniesOfExpiringJobs();
}