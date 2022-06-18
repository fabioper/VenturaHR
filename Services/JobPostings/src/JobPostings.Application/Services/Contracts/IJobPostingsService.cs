using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;
using JobPostings.CrossCutting.Filters;
using JobPostings.Domain.Aggregates.JobApplications;

namespace JobPostings.Application.Services.Contracts;

public interface IJobPostingsService
{
    Task CreateJobPosting(Guid companyId, CreateJobPostingRequest request);
    Task<FilterResponse<JobPostingResponse>> GetJobPostings(JobPostingsFilter filter);
    Task<JobPostingResponse> GetJobPostingOfId(Guid jobPostingId);
    Task<IEnumerable<JobApplicationResponse>> GetJobPostingApplications(Guid companyId, Guid jobPostingId);
    Task UpdateJob(Guid jobPostingId, UpdateJobRequest request);
    Task RenewJobPosting(Guid jobPostingId, RenewJobPostingRequest request);
}