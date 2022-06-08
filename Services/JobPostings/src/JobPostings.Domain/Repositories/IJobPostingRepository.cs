using Common.Abstractions;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Repositories;

public interface IJobPostingRepository : IBaseRepository<JobPosting>
{
    new Task<IEnumerable<JobPosting>> GetAll();
    new Task<JobPosting?> FindById(Guid id);
}