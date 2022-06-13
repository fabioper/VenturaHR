using JobPostings.Domain.Services.Contracts;
using Quartz;

namespace JobPostings.Infra.Jobs;

public class UpdateJobPostingStatusJob : IJob
{
    private readonly IJobPostingExpirationService _jobPostingExpirationService;

    public UpdateJobPostingStatusJob(IJobPostingExpirationService jobPostingExpirationService)
        => _jobPostingExpirationService = jobPostingExpirationService;

    public async Task Execute(IJobExecutionContext context)
    {
        await _jobPostingExpirationService.UpdateStatusOfExpiredJobs();
        await _jobPostingExpirationService.UpdateStatusOfJobsExpiredMoreThanLimit();
    }
}