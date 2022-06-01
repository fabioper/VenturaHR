using Common.Abstractions;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Repositories;

public interface IJobApplicationRepository : IBaseRepository<JobApplication, JobApplicationId>
{
    Task<IEnumerable<JobApplication>> GetAllByJobCompanyOfId(CompanyId companyId, JobPostingId jobPostingId);
    Task<IEnumerable<JobApplication>> GetAllByApplicant(ApplicantId applicantId);
}