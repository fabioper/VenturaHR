namespace JobPostings.Domain.Services.Contracts;

public interface IJobPostingStatusService
{
    Task NotifyCompaniesOfExpiringJobs();

    Task UpdateStatusOfExpiredJobs();
    Task UpdateStatusOfJobsExpiredMoreThanLimit();
}