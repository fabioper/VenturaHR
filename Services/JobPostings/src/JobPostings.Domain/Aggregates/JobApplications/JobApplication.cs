using Common.Abstractions;
using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Aggregates.JobPostings;

#nullable disable

namespace JobPostings.Domain.Aggregates.JobApplications;

public class JobApplication : BaseEntity<JobApplicationId>, IAggregateRoot
{
    public JobPosting JobPosting { get; }
    public Applicant Applicant { get; }
    public DateTime ApplicationDate { get; }

    public JobApplication(JobPosting jobPosting, Applicant applicant)
    {
        JobPosting = jobPosting;
        Applicant = applicant;
        ApplicationDate = DateTime.UtcNow;
    }

    public JobApplication() { } // Ef required
}