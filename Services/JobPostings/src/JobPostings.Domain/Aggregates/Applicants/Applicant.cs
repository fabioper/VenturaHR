#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Aggregates.Applicants;

public class Applicant : BaseEntity, IAggregateRoot
{
    public string Name { get; }

    public Applicant(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Applicant() { } // Ef required

    public JobApplication ApplyTo(JobPosting jobPosting, List<CriteriaAnswer> criteriaAnswers)
    {
        return new JobApplication(this, jobPosting, criteriaAnswers);
    }
}