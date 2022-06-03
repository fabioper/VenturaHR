using AutoMapper;
using Common.Exceptions;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Aggregates.JobPostings;
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
        IMapper mapper,
        IJobPostingRepository jobPostingRepository,
        IApplicantRepository applicantRepository)
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

    public async Task Apply(JobApplicationRequest request)
    {
        var applicant = await FindApplicantOfId(request);
        var jobPosting = await FindJobPostingOfId(request);
        var answers = MapCriteriasAnswers(request.CriteriaAnswers);

        var newApplication = new JobApplication(applicant, jobPosting, answers);
        await _applicationRepository.Add(newApplication);
    }

    private async Task<Applicant> FindApplicantOfId(JobApplicationRequest request)
    {
        var applicant = await _applicantRepository.FindById(request.ApplicantId);
        return applicant ?? throw new EntityNotFoundException(nameof(Applicant));
    }

    private async Task<JobPosting> FindJobPostingOfId(JobApplicationRequest request)
    {
        var jobPosting = await _jobPostingRepository.FindById(request.JobPostingId);
        return jobPosting ?? throw new EntityNotFoundException(nameof(JobPosting));
    }

    private static List<CriteriaAnswer> MapCriteriasAnswers(IEnumerable<CriteriaAnswerRequest> criteriaAnswers)
    {
        return criteriaAnswers.Select(c => new CriteriaAnswer(c.CriteriaId, c.Value)).ToList();
    }
}