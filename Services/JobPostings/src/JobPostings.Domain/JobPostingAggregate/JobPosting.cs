using Common.Abstractions;
using Common.Guards;
using JobPostings.Domain.CompanyAggregate;

namespace JobPostings.Domain.JobPostingAggregate;

#nullable disable

public class JobPosting : Entity, IAggregateRoot
{
    public string Description { get; private set; }

    public Role Role { get; private set; }

    public Salary Salary { get; private set; }

    public ExpirationDate ExpireAt { get; private set; }

    public Location Location { get; private set; }

    private readonly string _companyExternalId;
    public Company Company { get; private set; }

    public JobPosting(
        string role,
        string description,
        string location,
        decimal salary,
        DateTime expiration,
        string companyId)
    {
        Guard.Against.NullOrEmpty(role, nameof(role));
        Guard.Against.NullOrEmpty(description, nameof(description));

        Id = Guid.NewGuid();
        Description = description;
        Role = new(role);
        Salary = new(salary);
        ExpireAt = new(expiration);
        Location = new(location);
        _companyExternalId = companyId;
    }

    public JobPosting() { } // Ef required
}