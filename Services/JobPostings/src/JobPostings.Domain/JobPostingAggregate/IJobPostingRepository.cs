using Common.Abstractions;

namespace JobPostings.Domain.JobPostingAggregate;

public interface IJobPostingRepository : IRepository<JobPosting>
{
}