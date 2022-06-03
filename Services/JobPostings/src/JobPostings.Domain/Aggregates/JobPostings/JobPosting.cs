using Common.Abstractions;
using Common.Guards;
using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Aggregates.Criterias;

namespace JobPostings.Domain.Aggregates.JobPostings;

#nullable disable

public class JobPosting : BaseEntity, IAggregateRoot
{
    public string Description { get; private set; }

    public string Title { get; private set; }

    public string Location { get; private set; }

    public Salary Salary { get; private set; }

    public DateTime ExpireAt { get; private set; }

    public Company Company { get; }

    private List<Criteria> _criterias;
    public IReadOnlyCollection<Criteria> Criterias => _criterias;

    public JobPosting(
        string title,
        string description,
        string location,
        decimal salary,
        DateTime expiration,
        List<Criteria> criterias,
        Company company)
    {
        Guard.Against.NullOrEmpty(title, nameof(title));
        Guard.Against.NullOrEmpty(description, nameof(description));
        Guard.Against.NullOrEmpty(location, nameof(location));

        Id = Guid.NewGuid();
        Description = description;
        Title = title;
        Location = location;
        Salary = new Salary(salary);
        Company = company;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        ExpireAt = expiration;
        _criterias = criterias;
    }

    public JobPosting() { } // Ef required

    public void UpdateDescription(string description)
    {
        Guard.Against.NullOrEmpty(description, nameof(description));
        Description = description;
    }

    public void UpdateTitle(string newTitle)
    {
        Guard.Against.NullOrEmpty(newTitle, nameof(newTitle));
        Title = newTitle;
    }

    public void UpdateSalary(decimal newSalary)
    {
        Salary = new Salary(newSalary);
    }

    public void UpdateCriterias(List<Criteria> criterias)
    {
        _criterias = criterias;
    }

    public void UpdateLocation(string newLocation)
    {
        Guard.Against.NullOrEmpty(newLocation, nameof(newLocation));
        Location = newLocation;
    }
}