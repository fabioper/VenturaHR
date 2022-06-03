using Common.Abstractions;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Repositories;

public interface IJobPostingRepository : IBaseRepository<JobPosting>
{
    Task<List<JobPosting>> FindByCompanyOfId(Guid companyId);

    new Task<JobPosting?> FindById(Guid id);
}