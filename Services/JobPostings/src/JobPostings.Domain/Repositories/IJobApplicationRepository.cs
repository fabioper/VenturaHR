using Common.Abstractions;
using JobPostings.Domain.Aggregates.JobApplications;

namespace JobPostings.Domain.Repositories;

public interface IJobApplicationRepository : IBaseRepository<JobApplication>
{
    Task<IEnumerable<JobApplication>> GetAllByJobCompanyOfId(Guid companyId, Guid jobPostingId);
    Task<IEnumerable<JobApplication>> GetAllByApplicant(Guid applicantId);
}