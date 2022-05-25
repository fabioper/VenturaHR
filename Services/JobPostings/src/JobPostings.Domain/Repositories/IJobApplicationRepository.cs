using Common.Abstractions;
using JobPostings.Domain.Aggregates.JobApplications;

namespace JobPostings.Domain.Repositories;

public interface IJobApplicationRepository : IBaseRepository<JobApplication, JobApplicationId>
{
}