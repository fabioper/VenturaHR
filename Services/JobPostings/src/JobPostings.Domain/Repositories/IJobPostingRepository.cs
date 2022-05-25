using Common.Abstractions;
using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Repositories;

public interface IJobPostingRepository : IBaseRepository<JobPosting, JobPostingId>
{
    Task<List<JobPosting>> FindByCompanyOfId(CompanyId companyId);
}