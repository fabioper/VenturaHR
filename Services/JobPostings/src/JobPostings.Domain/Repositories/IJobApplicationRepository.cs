using Common.Abstractions;
using JobPostings.Domain.Aggregates.Applications;
using ApplicationId = JobPostings.Domain.Common.ApplicationId;

namespace JobPostings.Domain.Repositories;

public interface IJobApplicationRepository : IBaseRepository<JobApplication, ApplicationId>
{
}