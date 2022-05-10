using Common;
using JobPostings.Domain.JobPostingAggregate;

#nullable disable

namespace JobPostings.Domain.CompanyAggregate;

public class Company : Entity, IAggregateRoot
{
    public string Name { get; private set; }

    private readonly List<JobPosting> _jobPostings;
    public IReadOnlyCollection<JobPosting> JobPostings => _jobPostings;

    public Company(string name)
    {
        Name = name;
        _jobPostings = new List<JobPosting>();
    }

    public Company() { } // Ef required

    public void AddJobPosting(
        string role,
        string description,
        string location,
        decimal compensation,
        DateTime expirationDate)
    {
        var jobPosting = new JobPosting(
            role,
            description,
            new Location(location),
            new Compensation(compensation),
            new ExpirationDate(expirationDate),
            this
        );
        _jobPostings.Add(jobPosting);
    }
}