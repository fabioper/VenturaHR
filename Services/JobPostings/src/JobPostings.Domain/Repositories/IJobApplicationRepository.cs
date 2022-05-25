using Common.Abstractions;
using JobPostings.Domain.Aggregates.Application;
using ApplicationId = JobPostings.Domain.Common.ApplicationId;

namespace JobPostings.Domain.Repositories;

public interface IJobApplicationRepository : IBaseRepository<Application, ApplicationId>
{
}