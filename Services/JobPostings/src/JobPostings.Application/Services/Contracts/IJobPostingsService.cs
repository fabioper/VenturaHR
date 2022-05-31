using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;

namespace JobPostings.Application.Services.Contracts;

public interface IJobPostingsService
{
    Task PublishJob(CreateJobPostingRequest postingRequest);
    Task<IEnumerable<JobPostingResponse>> GetPublishedJobsBy(Guid companyId);
    Task<JobPostingResponse> GetJobPostingOfId(Guid jobPostingId);
    Task<IEnumerable<ApplicationResponse>> GetJobPostingApplications(Guid id);
    Task UpdateJob(UpdateJobRequest request);
}