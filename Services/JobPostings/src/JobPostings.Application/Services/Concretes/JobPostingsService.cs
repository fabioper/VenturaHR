using AutoMapper;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.Aggregates.Criterias;
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

    public async Task PublishJob(CreateJobPostingRequest postingRequest)
    {
        var company = await _companyRepository.FindById(new CompanyId(postingRequest.CompanyId));
        var criterias = _mapper.Map<List<Criteria>>(postingRequest.Criterias);

        var newJob = new JobPosting(postingRequest.Title,
            postingRequest.Description,
            postingRequest.Location,
            postingRequest.Salary,
            postingRequest.ExpirationDate,
            company,
            criterias);

        await _jobPostingsRepository.Add(newJob);
    }

    public async Task<IEnumerable<JobPostingResponse>> GetPublishedJobsBy(Guid companyId)
    {
        var jobPostings = await _jobPostingsRepository.FindByCompanyOfId(new CompanyId(companyId));
        return _mapper.Map<IEnumerable<JobPostingResponse>>(jobPostings);
    }
}