using JobPostings.CrossCutting.Filters;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Repositories;
using JobPostings.Domain.Validators;

namespace JobPostings.Infra.Validators;

public class DuplicateApplicationValidator : IDuplicateApplicationValidator
{
    private readonly IJobApplicationRepository _jobApplicationRepository;

    public DuplicateApplicationValidator(IJobApplicationRepository jobApplicationRepository)
        => _jobApplicationRepository = jobApplicationRepository;

    public bool IsValid(JobApplication entity, out IDictionary<string, string> brokenRules)
    {
        brokenRules = BrokenRules(entity);
        return !brokenRules.Any();
    }

    public IDictionary<string, string> BrokenRules(JobApplication entity)
    {
        var brokenRules = new Dictionary<string, string>();

        var filter = new ApplicationsFilter { Applicant = entity.Applicant.Id};
        var applicantApplications = _jobApplicationRepository.GetAll(filter).Result;
        var alreadyApplied = applicantApplications.Any(x => x.JobPosting.Id == entity.JobPosting.Id);

        if (alreadyApplied)
            brokenRules.Add(nameof(entity.JobPosting), "Applicant already applied to this Job Posting");

        return brokenRules;
    }
}