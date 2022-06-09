using Common.Abstractions;
using JobPostings.CrossCutting.Filters;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Repositories;

public interface IJobPostingRepository : IBaseRepository<JobPosting>
{
    Task<IEnumerable<JobPosting>> GetAll(JobPostingsFilter filter);
    new Task<JobPosting?> FindById(Guid id);
    Task<int> Count(JobPostingsFilter baseFilter);
    Task<IEnumerable<JobPosting>> GetAllJobsAboutToExpire();
}