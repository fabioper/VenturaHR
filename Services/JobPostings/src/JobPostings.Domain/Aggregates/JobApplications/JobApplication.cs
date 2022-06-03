#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Aggregates.JobApplications;

public class JobApplication : BaseEntity, IAggregateRoot
{
    public JobPosting JobPosting { get; }

    public Applicant Applicant { get; }

    private readonly List<CriteriaAnswer> _criteriasAnswers;

    public IReadOnlyCollection<CriteriaAnswer> CriteriasAnswers
        => _criteriasAnswers;

    public JobApplication(
        Applicant applicant,
        JobPosting jobPosting,
        List<CriteriaAnswer> criteriasAnswers)
    {
        Id = Guid.NewGuid();
        JobPosting = jobPosting;
        Applicant = applicant;
        _criteriasAnswers = criteriasAnswers;
    }

    public JobApplication() { } // Ef required
}