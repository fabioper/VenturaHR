#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Aggregates.Application;

public class Application : BaseEntity<ApplicationId>, IAggregateRoot
{
    public JobPosting JobPosting { get; }
    public Applicant Applicant { get; }
    public DateTime ApplicationDate { get; }

    public Application(JobPosting jobPosting, Applicant applicant)
    {
        JobPosting = jobPosting;
        Applicant = applicant;
        ApplicationDate = DateTime.UtcNow;
    }

    public Application() { } // Ef required
}