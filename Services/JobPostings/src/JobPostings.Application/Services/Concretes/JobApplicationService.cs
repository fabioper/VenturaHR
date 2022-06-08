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

        var application = applicant.ApplyTo(jobPosting, MapCriteriaAnswers(request.CriteriaAnswers));
        
        // TODO: Validar que o usuário não tenha aplicado para a mesma vaga antes.
        // TODO: Validar que o usuário respondeu a todos os critérios adicionados a vaga
        
        await _applicationRepository.Add(application);
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