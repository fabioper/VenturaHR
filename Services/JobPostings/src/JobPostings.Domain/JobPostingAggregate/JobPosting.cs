using Common.Abstractions;
using Common.Guards;
using JobPostings.Domain.CompanyAggregate;

namespace JobPostings.Domain.JobPostingAggregate;

#nullable disable

public class JobPosting : BaseEntity<JobPostingId>, IAggregateRoot
{
    public string Description { get; }

    public Role Role { get; }

    public Salary Salary { get; }

    public ExpirationDate ExpireAt { get; }

    public Location Location { get; }

    public Company Company { get; }

    public DateTime CreatedAt { get; }
    
    public DateTime UpdatedAt { get; }

    public JobPosting(
        string role,
        string description,
        string location,
        decimal salary,
        DateTime expiration,
        Company company)
    {
        Guard.Against.NullOrEmpty(role, nameof(role));
        Guard.Against.NullOrEmpty(description, nameof(description));

        Id = new JobPostingId();
        Description = description;
        Role = new Role(role);
        Salary = new Salary(salary);
        ExpireAt = new ExpirationDate(expiration);
        Location = new Location(location);
        Company = company;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // Ef required
    public JobPosting() { }
}