using Common.Abstractions;
using Common.Guards;

namespace JobPostings.Domain.JobPostingAggregate;

#nullable disable

public class JobPosting : Entity, IAggregateRoot
{
    public string Role { get; private set; }

    public string Description { get; private set; }

    public Salary Salary { get; private set; }

    public ExpirationDate ExpireAt { get; private set; }

    public Location Location { get; private set; }

    public CompanyId CompanyId { get; private set; }

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
        Role = role;
        Description = description;
        Salary = new(salary);
        ExpireAt = new(expiration);
        Location = new(location);
        CompanyId = new(companyId);
    }

    public JobPosting() { } // Ef required
}