using Common.Abstractions;
using Common.Guards;
using JobPostings.Domain.CompanyAggregate;

namespace JobPostings.Domain.JobPostingAggregate;

#nullable disable

public class JobPosting : Entity, IAggregateRoot
{
    public string Role { get; private set; }

    public string Description { get; private set; }

    public Salary Salary { get; private set; }

    public ExpirationDate ExpireAt { get; private set; }

    public Location Location { get; private set; }

    public Guid _companyId;
    public Company Company { get; private set; }

    public JobPosting(
        string id,
        string role,
        string description,
        Location location,
        Salary salary,
        ExpirationDate expiration,
        Company company)
    {
        Guard.Against.NullOrEmpty(role, nameof(role));
        Guard.Against.NullOrEmpty(description, nameof(description));

        Id = Guid.Parse(id);
        Role = role;
        Description = description;
        Salary = salary;
        ExpireAt = expiration;
        Location = location;
        Company = company;
    }

    public JobPosting() { } // Ef required
}