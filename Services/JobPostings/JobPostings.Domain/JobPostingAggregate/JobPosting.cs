using Common;
using Common.Guards;
using JobPostings.Domain.CompanyAggregate;

namespace JobPostings.Domain.JobPostingAggregate;

public class JobPosting : Entity, IAggregateRoot
{
    private long _companyId;

    public Company Company;

    public string Role { get; }

    public string Description { get; }

    public Compensation Compensation { get; }

    public ExpirationDate ExpireAt { get; }

    public Location Location { get; }

    public JobPosting(
        string role,
        string description,
        Compensation compensation,
        ExpirationDate expiration,
        Location location)
    {
        Guard.Against.NullOrEmpty(role, nameof(role));
        Guard.Against.NullOrEmpty(description, nameof(description));

        Role = role;
        Description = description;
        Compensation = compensation;
        ExpireAt = expiration;
        Location = location;
    }

    public JobPosting() { }
}