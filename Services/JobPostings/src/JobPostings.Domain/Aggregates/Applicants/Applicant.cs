#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Exceptions;

namespace JobPostings.Domain.Aggregates.Applicants;

public class Applicant : BaseEntity, IAggregateRoot
{
    public string Name { get; }

    public Applicant(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Applicant() { }

    public JobApplication ApplyTo(JobPosting jobPosting, List<CriteriaAnswer> criteriaAnswers)
    {
        if (jobPosting.HasExpired)
            throw new ExpiredJobPostingException();

        return new JobApplication(this, jobPosting, criteriaAnswers);
    }
}