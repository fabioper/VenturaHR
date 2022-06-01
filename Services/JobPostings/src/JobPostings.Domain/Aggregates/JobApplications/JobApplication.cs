#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Aggregates.JobApplications;

public class JobApplication : BaseEntity<JobApplicationId>, IAggregateRoot
{
    public JobPosting JobPosting { get; }

    public Applicant Applicant { get; }

    public DateTime AppliedAt { get; }

    private List<CriteriaAnswer> _criteriasAnswers;

    public IReadOnlyCollection<CriteriaAnswer> CriteriasAnswers
        => _criteriasAnswers;

    public JobApplication(
        Applicant applicant,
        JobPosting jobPosting,
        List<CriteriaAnswer> criteriasAnswers)
    {
        Id = new JobApplicationId();
        JobPosting = jobPosting;
        Applicant = applicant;
        AppliedAt = DateTime.UtcNow;
        _criteriasAnswers = criteriasAnswers;
    }

    public JobApplication() { } // Ef required
}