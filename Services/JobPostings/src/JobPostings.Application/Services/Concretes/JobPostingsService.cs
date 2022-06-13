using AutoMapper;
using Common.Exceptions;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Application.Services.Contracts;
using JobPostings.CrossCutting.Extensions;
using JobPostings.CrossCutting.Filters;
using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Aggregates.JobPostings;
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

        var newJob = company.PublishJob(
            request.Title,
            request.Description,
            request.Location,
            request.ExpirationDate,
            request.Salary,
            MapCriterias(request.Criterias)
        );

        await _jobPostingsRepository.Add(newJob);
    }

    public async Task UpdateJob(Guid jobPostingId, UpdateJobRequest request)
    {
        var jobPosting = await FindJobPostingOfId(jobPostingId);

        jobPosting.UpdateTitle(request.Title);
        jobPosting.UpdateDescription(request.Description);
        jobPosting.UpdateLocation(request.Location);
        jobPosting.UpdateSalary(request.Salary);
        jobPosting.UpdateCriterias(MapCriterias(request.Criterias));

        await _jobPostingsRepository.Update(jobPosting);
    }

    public async Task RenewJobPosting(Guid jobPostingId, RenewJobPostingRequest request)
    {
        var jobPosting = await FindJobPostingOfId(jobPostingId);
        jobPosting.Renew(request.NewExpiration);
        await _jobPostingsRepository.Update(jobPosting);
    }

    public async Task<FilterResponse<JobPostingResponse>> GetJobPostings(JobPostingsFilter filter)
    {
        var jobPostings = await _jobPostingsRepository.GetAll(filter);
        var totalRecords = await _jobPostingsRepository.Count(filter);
        var results = _mapper.Map<List<JobPostingResponse>>(jobPostings);

        return results.ToFilterResponse(filter, totalRecords);
    }

    public async Task<JobPostingResponse> GetJobPostingOfId(Guid jobPostingId)
    {
        var jobPosting = await FindJobPostingOfId(jobPostingId);
        return _mapper.Map<JobPostingResponse>(jobPosting);
    }

    public async Task<IEnumerable<JobApplication>> GetJobPostingApplications(Guid companyId, Guid jobPostingId)
    {
        var applications = await _applicationRepository.GetAllByJobCompanyOfId(companyId, jobPostingId);
        return _mapper.Map<List<JobApplication>>(applications);
    }

    private async Task<JobPosting> FindJobPostingOfId(Guid jobPostingId)
    {
        var jobPosting = await _jobPostingsRepository.FindById(jobPostingId);
        return jobPosting ?? throw new EntityNotFoundException(nameof(JobPosting));
    }

    private async Task<Company> FindCompanyOfId(Guid companyId)
    {
        var company = await _companyRepository.FindById(companyId);
        return company ?? throw new EntityNotFoundException(nameof(Company));
    }

    private static List<Criteria> MapCriterias(IEnumerable<CriteriaRequest> criterias)
    {
        return criterias.Select(c => new Criteria(c.Title, c.Description, c.Weight, c.DesiredProfile)).ToList();
    }
}