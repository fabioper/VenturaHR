using AutoMapper;
using Common.Exceptions;
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
    private readonly IJobApplicationRepository _applicationRepository;
    private readonly IMapper _mapper;

    public JobPostingsService(
        IJobPostingRepository jobPostingsRepository,
        ICompanyRepository companyRepository,
        IMapper mapper,
        IJobApplicationRepository applicationRepository)
    {
        _jobPostingsRepository = jobPostingsRepository;
        _companyRepository = companyRepository;
        _mapper = mapper;
        _applicationRepository = applicationRepository;
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

    public async Task<JobPostingResponse> GetJobPostingOfId(Guid jobPostingId)
    {
        var jobPosting = await _jobPostingsRepository.FindById(new JobPostingId(jobPostingId));

        if (jobPosting is null)
            throw new EntityNotFoundException(nameof(jobPosting));

        return _mapper.Map<JobPostingResponse>(jobPosting);
    }

    public async Task<IEnumerable<ApplicationResponse>> GetJobPostingApplications(Guid id)
    {
        var jobPostingId = new JobPostingId(id);

        var jobPosting = await _jobPostingsRepository.FindById(jobPostingId);
        if (jobPosting is null)
            throw new EntityNotFoundException(nameof(JobPosting));

        var applications = await _applicationRepository.GetAllByJobCompanyOfId(jobPostingId);
        return _mapper.Map<List<ApplicationResponse>>(applications);
    }

    public async Task UpdateJob(UpdateJobRequest request)
    {
        var jobPosting = await _jobPostingsRepository.FindById(new JobPostingId(request.Id));
        if (jobPosting is null)
            throw new EntityNotFoundException(nameof(JobPosting));

        jobPosting.UpdateDescription(request.Description);
        jobPosting.UpdateTitle(request.Title);
        jobPosting.UpdateSalary(request.Salary);

        var criterias = _mapper.Map<List<Criteria>>(request.Criterias);
        jobPosting.UpdateCriterias(criterias);

        await _jobPostingsRepository.Update(jobPosting);
    }
}