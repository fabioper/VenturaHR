using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.CompanyAggregate;
using JobPostings.Domain.JobPostingAggregate;

namespace JobPostings.Application.Services.Concretes;

public class JobPostingsService : IJobPostingsService
{
    private readonly IJobPostingRepository _jobPostingsRepository;
    private readonly ICompanyRepository _companyRepository;

    public JobPostingsService(IJobPostingRepository jobPostingsRepository, ICompanyRepository companyRepository)
    {
        _jobPostingsRepository = jobPostingsRepository;
        _companyRepository = companyRepository;
    }

    public async Task PublishJob(PublishJobRequest request)
    {
        var company = await _companyRepository.FindById(new CompanyId(request.CompanyId));

        var newJob = new JobPosting(request.Role,
            request.Description,
            request.Location,
            request.Salary,
            request.ExpirationDate,
            company);

        await _jobPostingsRepository.Add(newJob);
    }

    public async Task<IEnumerable<JobPosting>> GetPublishedJobsBy(string companyId)
    {
        return await _jobPostingsRepository.FindByCompanyOfId(new CompanyId(companyId));
    }
}