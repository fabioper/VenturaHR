using Common.Abstractions;
using JobPostings.Domain.Aggregates.Companies;

namespace JobPostings.Domain.Aggregates.JobPostings;

public interface IJobPostingRepository : IBaseRepository<JobPosting, JobPostingId>
{
    Task<List<JobPosting>> FindByCompanyOfId(CompanyId companyId);
}