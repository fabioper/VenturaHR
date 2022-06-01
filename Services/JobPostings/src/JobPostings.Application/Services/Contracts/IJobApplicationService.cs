using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;

namespace JobPostings.Application.Services.Contracts;

public interface IJobApplicationService
{
    Task<IEnumerable<JobApplicationResponse>> GetApplicationsFrom(Guid applicantId);
    Task Apply(JobApplicationRequest request);
}