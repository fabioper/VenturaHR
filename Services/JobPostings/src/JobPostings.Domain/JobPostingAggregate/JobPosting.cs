using Common.Abstractions;
using Common.Guards;
using JobPostings.Domain.CompanyAggregate;

namespace JobPostings.Domain.JobPostingAggregate;

#nullable disable

public class JobPosting : BaseEntity<JobPostingId>, IAggregateRoot
{
    public string Description { get; private set; }

    public Role Role { get; private set; }

    public Salary Salary { get; private set; }

    public ExpirationDate ExpireAt { get; private set; }

    public Location Location { get; private set; }

    public Company Company { get; private set; }

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
    }

    public JobPosting() { } // Ef required
}