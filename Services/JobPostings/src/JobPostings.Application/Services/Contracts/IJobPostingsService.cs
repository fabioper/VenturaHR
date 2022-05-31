using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;

namespace JobPostings.Application.Services.Contracts;

public interface IJobPostingsService
{
    Task CreateJobPosting(Guid companyId, CreateJobPostingRequest request);
    Task<IEnumerable<JobPostingResponse>> GetPublishedJobsBy(Guid companyId);
    Task<JobPostingResponse> GetJobPostingOfId(Guid jobPostingId);
    Task<IEnumerable<ApplicationResponse>> GetJobPostingApplications(Guid id);
    Task UpdateJob(Guid jobPostingId, UpdateJobRequest request);
}