#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Aggregates.JobApplications;

public class JobApplication : BaseEntity, IAggregateRoot
{
    public JobPosting JobPosting { get; private set; }

    public Applicant Applicant { get; private set; }

    public double Average { get; private set; }

    public List<CriteriaAnswer> Answers { get; private set; }

    internal JobApplication(
        Applicant applicant,
        JobPosting jobPosting,
        List<CriteriaAnswer> answers)
    {
        Id = Guid.NewGuid();
        JobPosting = jobPosting;
        Applicant = applicant;
        Answers = answers;
        Average = CalculateAverage();
    }

    private double CalculateAverage()
    {
        if (!Answers.Any())
            return 0;

        return Answers.Sum(x => x.Value * x.Criteria.Weight) / (double)Answers.Sum(x => x.Criteria.Weight);
    }

    public JobApplication() { } // Ef required
}