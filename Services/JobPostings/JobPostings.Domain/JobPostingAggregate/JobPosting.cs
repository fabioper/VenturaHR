using Common;
using Common.Guards;
using JobPostings.Domain.CompanyAggregate;

namespace JobPostings.Domain.JobPostingAggregate;

#nullable disable

public class JobPosting : Entity, IAggregateRoot
{
    public string Role { get; private set; }

    public string Description { get; private set; }

    public Compensation Compensation { get; private set; }

    public ExpirationDate ExpireAt { get; private set; }

    public Location Location { get; private set; }

    public Company Company { get; private set; }

    public JobPosting(
        string role,
        string description,
        Location location,
        Compensation compensation,
        ExpirationDate expiration,
        Company company)
    {
        Guard.Against.NullOrEmpty(role, nameof(role));
        Guard.Against.NullOrEmpty(description, nameof(description));

        Role = role;
        Description = description;
        Compensation = compensation;
        ExpireAt = expiration;
        Location = location;
        Company = company;
    }

    public JobPosting() { } // Ef required

    public virtual bool IsExpired => ExpireAt.HasPassed(DateTime.Now);
}