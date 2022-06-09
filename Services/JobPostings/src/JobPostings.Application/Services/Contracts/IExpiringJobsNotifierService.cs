namespace JobPostings.Application.Services.Contracts;

public interface IExpiringJobsNotifierService
{
    Task NotifyCompaniesOfExpiringJobs();
}