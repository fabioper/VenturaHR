using Common.Abstractions;
using JobPostings.Domain.CompanyAggregate;

namespace JobPostings.Domain.JobPostingAggregate;

public interface IJobPostingRepository : IBaseRepository<JobPosting, JobPostingId>
{
    Task<List<JobPosting>> FindByCompanyOfId(CompanyId companyId);
}