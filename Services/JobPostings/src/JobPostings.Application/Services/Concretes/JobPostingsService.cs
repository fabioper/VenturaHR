using AutoMapper;
using Common.Exceptions;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.Aggregates.Companies;
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
        IJobApplicationRepository applicationRepository,
        IMapper mapper)
    {
        _jobPostingsRepository = jobPostingsRepository;
        _companyRepository = companyRepository;
        _mapper = mapper;
        _applicationRepository = applicationRepository;
    }

    public async Task CreateJobPosting(Guid companyId, CreateJobPostingRequest request)
    {
        var company = await FindCompanyOfId(companyId);

        var newJob = new JobPosting(request.Title,
            request.Description,
            request.Location,
            request.Salary,
            request.ExpirationDate,
            MapCriterias(request.Criterias),
            company);

        await _jobPostingsRepository.Add(newJob);
    }

    public async Task<IEnumerable<JobPostingResponse>> GetPublishedJobsBy(Guid companyId)
    {
        var jobPostings = await _jobPostingsRepository.FindByCompanyOfId(new CompanyId(companyId));
        return _mapper.Map<IEnumerable<JobPostingResponse>>(jobPostings);
    }

    public async Task<JobPostingResponse> GetJobPostingOfId(Guid jobPostingId)
    {
        var jobPosting = await FindJobPostingOfId(jobPostingId);
        return _mapper.Map<JobPostingResponse>(jobPosting);
    }

    public async Task<IEnumerable<ApplicationResponse>> GetJobPostingApplications(Guid id)
    {
        var jobPosting = await FindJobPostingOfId(id);
        var applications = await _applicationRepository.GetAllByJobCompanyOfId(jobPosting.Id);
        return _mapper.Map<List<ApplicationResponse>>(applications);
    }

    public async Task UpdateJob(Guid jobPostingId, UpdateJobRequest request)
    {
        var jobPosting = await FindJobPostingOfId(jobPostingId);
        var criterias = _mapper.Map<List<Criteria>>(request.Criterias);

        jobPosting.UpdateDescription(request.Description);
        jobPosting.UpdateTitle(request.Title);
        jobPosting.UpdateSalary(request.Salary);
        jobPosting.UpdateLocation(request.Location);
        jobPosting.UpdateCriterias(criterias);

        await _jobPostingsRepository.Update(jobPosting);
    }

    private async Task<JobPosting> FindJobPostingOfId(Guid id)
    {
        var jobPosting = await _jobPostingsRepository.FindById(new JobPostingId(id));
        return jobPosting ?? throw new EntityNotFoundException(nameof(JobPosting));
    }

    private async Task<Company> FindCompanyOfId(Guid companyId)
    {
        var company = await _companyRepository.FindById(new CompanyId(companyId));
        return company ?? throw new EntityNotFoundException(nameof(Company));
    }

    private List<Criteria> MapCriterias(IEnumerable<CriteriaRequest> criterias)
        => _mapper.Map<List<Criteria>>(criterias);
}