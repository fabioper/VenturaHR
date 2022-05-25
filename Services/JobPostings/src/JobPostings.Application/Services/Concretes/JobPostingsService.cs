using AutoMapper;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Common;
using JobPostings.Domain.Repositories;

namespace JobPostings.Application.Services.Concretes;

public class JobPostingsService : IJobPostingsService
{
    private readonly IJobPostingRepository _jobPostingsRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public JobPostingsService(
        IJobPostingRepository jobPostingsRepository,
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _jobPostingsRepository = jobPostingsRepository;
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task PublishJob(PostJobRequest request)
    {
        var company = await _companyRepository.FindById(new CompanyId(request.CompanyId));

        var newJob = new JobPosting(request.Title,
            request.Description,
            request.Location,
            request.Salary,
            request.ExpirationDate,
            company);

        await _jobPostingsRepository.Add(newJob);
    }

    public async Task<IEnumerable<JobPostingResponse>> GetPublishedJobsBy(string companyId)
    {
        var jobPostings = await _jobPostingsRepository.FindByCompanyOfId(new CompanyId(companyId));
        return _mapper.Map<IEnumerable<JobPostingResponse>>(jobPostings);
    }
}