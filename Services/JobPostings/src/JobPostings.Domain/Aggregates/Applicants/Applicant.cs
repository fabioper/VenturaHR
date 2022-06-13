#nullable disable

using Common.Abstractions;
using JobPostings.CrossCutting.Exceptions;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Aggregates.Applicants;

public class Applicant : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    public Applicant(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public Applicant()
    {
    }

    public JobApplication ApplyTo(JobPosting jobPosting, List<CriteriaAnswer> criteriaAnswers)
    {
        if (!jobPosting.CanBeApplied)
            throw new UnableToApplyException();

        return new JobApplication(this, jobPosting, criteriaAnswers);
    }
}