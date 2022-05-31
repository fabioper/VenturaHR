using Common.Abstractions;
using JobPostings.Domain.Aggregates.Applications;
using JobPostings.Domain.Common;
using ApplicationId = JobPostings.Domain.Common.ApplicationId;

namespace JobPostings.Domain.Repositories;

public interface IJobApplicationRepository : IBaseRepository<JobApplication, ApplicationId>
{
    Task<IEnumerable<JobApplication>> GetAllByJobCompanyOfId(JobPostingId jobPostingId);
}