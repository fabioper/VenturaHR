using Common.Abstractions;
using Common.Guards;
using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Aggregates.JobPostings;

#nullable disable

public class JobPosting : BaseEntity<JobPostingId>, IAggregateRoot
{
    public string Description { get; }

    public string Title { get; }

    public string Location { get; }

    public Salary Salary { get; }

    public Company Company { get; }

    public DateTime CreatedAt { get; }
    
    public DateTime UpdatedAt { get; }
    
    public DateTime ExpireAt { get; }

    private List<Criteria> _criterias;
    public IReadOnlyCollection<Criteria> Criterias => _criterias;

    public JobPosting(
        string title,
        string description,
        string location,
        decimal salary,
        DateTime expiration,
        Company company,
        List<Criteria> criterias)
    {
        Guard.Against.NullOrEmpty(title, nameof(title));
        Guard.Against.NullOrEmpty(description, nameof(description));
        Guard.Against.NullOrEmpty(location, nameof(location));

        Id = new JobPostingId();
        Description = description;
        Title = title;
        Location = location;
        Salary = new Salary(salary);
        Company = company;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        ExpireAt = expiration;
        _criterias = new List<Criteria>();
    }

    // Ef required
    public JobPosting() { }
}