using JobPostings.Application.DTOs.Requests;
using JobPostings.Domain.JobPostingAggregate;

namespace JobPostings.Application.Services.Contracts;

public interface IJobPostingsService
{
    Task PublishJob(PublishJobRequest request);
    Task<IEnumerable<JobPosting>> GetPublishedJobsBy(string companyId);
}