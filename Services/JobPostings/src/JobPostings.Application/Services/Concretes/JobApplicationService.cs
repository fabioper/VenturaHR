using AutoMapper;
using Common.Exceptions;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Exceptions;
using JobPostings.Domain.Repositories;

namespace JobPostings.Application.Services.Concretes;

public class JobApplicationService : IJobApplicationService
{
    private readonly IJobApplicationRepository _applicationRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IJobPostingRepository _jobPostingRepository;
    private readonly IMapper _mapper;

    public JobApplicationService(
        IJobApplicationRepository applicationRepository,
        IApplicantRepository applicantRepository,
        IJobPostingRepository jobPostingRepository,
        IMapper mapper)
    {
        _applicationRepository = applicationRepository;
        _mapper = mapper;
        _jobPostingRepository = jobPostingRepository;
        _applicantRepository = applicantRepository;
    }

    public async Task<IEnumerable<JobApplicationResponse>> GetApplicationsFrom(Guid applicantId)
    {
        var applications = await _applicationRepository.GetAllByApplicant(applicantId);
        return _mapper.Map<List<JobApplicationResponse>>(applications);
    }

    public async Task Apply(Guid applicantId, JobApplicationRequest request)
    {
        var applicant = await FindApplicantOfId(applicantId);
        var jobPosting = await FindJobPostingOfId(request.JobPostingId);

        var alreadyApplied = await CheckIfAlreadyApplied(applicantId, jobPosting);
        if (alreadyApplied)
            throw new JobPostingAlreadyAppliedException();

        var criteriaAnswers = MapCriteriaAnswers(request.CriteriaAnswers);
        var application = applicant.ApplyTo(jobPosting, criteriaAnswers);

        await _applicationRepository.Add(application);
    }

    private async Task<bool> CheckIfAlreadyApplied(Guid applicantId, JobPosting jobPosting)
    {
        var applications = await _applicationRepository.GetAllByApplicant(applicantId);
        var alreadyApplied = applications.Any(application => application.JobPosting == jobPosting);
        return alreadyApplied;
    }

    private static List<CriteriaAnswer> MapCriteriaAnswers(IEnumerable<CriteriaAnswerRequest> criteriaAnswers)
        => criteriaAnswers.Select(c => new CriteriaAnswer(c.CriteriaId, c.Value)).ToList();

    private async Task<Applicant> FindApplicantOfId(Guid applicantId)
    {
        var applicant = await _applicantRepository.FindById(applicantId);
        return applicant ?? throw new EntityNotFoundException(nameof(Applicant));
    }

    private async Task<JobPosting> FindJobPostingOfId(Guid jobPostingId)
    {
        var jobPosting = await _jobPostingRepository.FindById(jobPostingId);
        return jobPosting ?? throw new EntityNotFoundException(nameof(JobPosting));
    }
}