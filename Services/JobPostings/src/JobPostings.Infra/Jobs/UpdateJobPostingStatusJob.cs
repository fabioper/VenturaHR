using JobPostings.Domain.Services.Contracts;
using Quartz;

namespace JobPostings.Infra.Jobs;

public class UpdateJobPostingStatusJob : IJob
{
    private readonly IJobPostingStatusService _jobPostingStatusService;

    public UpdateJobPostingStatusJob(IJobPostingStatusService jobPostingStatusService)
        => _jobPostingStatusService = jobPostingStatusService;

    public async Task Execute(IJobExecutionContext context)
    {
        await _jobPostingStatusService.UpdateStatusOfExpiredJobs();
        await _jobPostingStatusService.UpdateStatusOfJobsExpiredMoreThanLimit();
    }
}