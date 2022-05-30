using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;

namespace JobPostings.Application.Services.Contracts;

public interface IJobPostingsService
{
    Task PublishJob(PostJobRequest request);
    Task<IEnumerable<JobPostingResponse>> GetPublishedJobsBy(string companyId);
}