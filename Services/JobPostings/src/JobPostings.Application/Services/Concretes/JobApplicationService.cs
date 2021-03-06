using System.Data;
using AutoMapper;
using Common.Exceptions;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Application.Extensions;
using JobPostings.Application.Services.Contracts;
using JobPostings.CrossCutting.Extensions;
using JobPostings.CrossCutting.Filters;
using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Repositories;
using JobPostings.Domain.Validators;

namespace JobPostings.Application.Services.Concretes;

public class JobApplicationService : IJobApplicationService
{
    private readonly IJobApplicationRepository _applicationRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IJobPostingRepository _jobPostingRepository;
    private readonly IDuplicateApplicationValidator _duplicateApplicationValidator;
    private readonly IMapper _mapper;

    public JobApplicationService(
        IJobApplicationRepository applicationRepository,
        IApplicantRepository applicantRepository,
        IJobPostingRepository jobPostingRepository,
        IDuplicateApplicationValidator duplicateApplicationValidator,
        IMapper mapper)
    {
        _applicationRepository = applicationRepository;
        _applicantRepository = applicantRepository;
        _jobPostingRepository = jobPostingRepository;
        _duplicateApplicationValidator = duplicateApplicationValidator;
        _mapper = mapper;
    }

    public async Task<FilterResponse<JobApplicationResponse>> GetAll(ApplicationsFilter filter)
    {
        var applications = await _applicationRepository.GetAll(filter);
        var totalRecords = await _applicationRepository.Count(filter);
        
        var results = _mapper.Map<List<JobApplicationResponse>>(applications);
        
        return results.ToFilterResponse(filter, totalRecords);
    }

    public async Task Apply(Guid applicantId, JobApplicationRequest request)
    {
        var applicant = await FindApplicantOfId(applicantId);
        var jobPosting = await FindJobPostingOfId(request.JobPostingId);
        var criteriaAnswers = MapCriteriaAnswers(jobPosting, request.CriteriaAnswers);

        var application = applicant.ApplyTo(jobPosting, criteriaAnswers.ToList());
        application.ValidateAgainst(_duplicateApplicationValidator);

        await _applicationRepository.Add(application);
    }

    private static IEnumerable<CriteriaAnswer> MapCriteriaAnswers(
        JobPosting jobPosting,
        IReadOnlyCollection<CriteriaAnswerRequest> criteriaAnswers)
    {
        foreach (var jobPostingCriteria in jobPosting.Criterias)
        {
            var answer = criteriaAnswers.FirstOrDefault(x => x.CriteriaId == jobPostingCriteria.Id);
            if (answer is null)
                throw new InvalidConstraintException();

            yield return new CriteriaAnswer(jobPostingCriteria, answer.Value);
        }
    }

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