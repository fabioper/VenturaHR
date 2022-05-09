using Common;
using Common.Guards;
using JobPostings.Domain.CompanyAggregate;

namespace JobPostings.Domain.JobPostingAggregate;

public class JobPosting : Entity, IAggregateRoot
{
    public string Role { get; private set; }

    public string Description { get; private set; }

    public Compensation Compensation { get; private set; }

    public ExpirationDate ExpireAt { get; private set; }

    public string Location { get; private set; }

    private long _companyId;

    public Company Company;

    public JobPosting(
        string role,
        string description,
        string location,
        Compensation compensation,
        ExpirationDate expiration)
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