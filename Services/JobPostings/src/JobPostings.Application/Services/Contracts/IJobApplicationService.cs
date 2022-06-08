using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;
using JobPostings.CrossCutting.Filters;

namespace JobPostings.Application.Services.Contracts;

public interface IJobApplicationService
{
    Task<FilterResponse<JobApplicationResponse>> GetAll(ApplicationsFilter filter);
    Task Apply(Guid applicantId, JobApplicationRequest request);
}