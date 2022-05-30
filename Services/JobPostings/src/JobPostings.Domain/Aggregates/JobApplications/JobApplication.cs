#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Aggregates.Application;
using JobPostings.Domain.Aggregates.JobPostings;
using ApplicationId = JobPostings.Domain.Common.ApplicationId;

namespace JobPostings.Domain.Aggregates.Applications;

public class JobApplication : BaseEntity<ApplicationId>, IAggregateRoot
{
    public JobPosting JobPosting { get; }

    public Applicant Applicant { get; }

    public DateTime AppliedAt { get; }

    private List<CriteriaFullfillment> _criteriasFullfillments;

    public IReadOnlyCollection<CriteriaFullfillment> CriteriasFullfillments
        => _criteriasFullfillments;

    public JobApplication(
        Applicant applicant,
        JobPosting jobPosting,
        List<CriteriaFullfillment> criteriasFullfillments)
    {
        JobPosting = jobPosting;
        Applicant = applicant;
        AppliedAt = DateTime.UtcNow;
        _criteriasFullfillments = criteriasFullfillments;
    }

    public JobApplication() { } // Ef required
}