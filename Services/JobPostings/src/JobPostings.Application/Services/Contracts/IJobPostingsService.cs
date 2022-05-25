using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Application.Services.Contracts;

public interface IJobPostingsService
{
    Task PublishJob(PostJobRequest request);
    Task<IEnumerable<JobPostingResponse>> GetPublishedJobsBy(string companyId);
}