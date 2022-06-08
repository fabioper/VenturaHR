using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;
using JobPostings.CrossCutting.Filters;

namespace JobPostings.Application.Services.Contracts;

public interface IJobPostingsService
{
    Task CreateJobPosting(Guid companyId, CreateJobPostingRequest request);
    Task<FilterResponse<JobPostingResponse>> GetJobPostings(BaseFilter filter);
    Task<JobPostingResponse> GetJobPostingOfId(Guid jobPostingId);
    Task<IEnumerable<ApplicationResponse>> GetJobPostingApplications(Guid companyId, Guid jobPostingId);
    Task UpdateJob(Guid jobPostingId, UpdateJobRequest request);
}