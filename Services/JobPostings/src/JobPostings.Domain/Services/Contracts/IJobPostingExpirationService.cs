namespace JobPostings.Domain.Services.Contracts;

public interface IJobPostingExpirationService
{
    Task NotifyCompaniesOfExpiringJobs();

    Task UpdateJobPostingsStatus();
}