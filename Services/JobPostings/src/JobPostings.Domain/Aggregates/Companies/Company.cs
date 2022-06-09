#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Aggregates.Companies;

public class Company : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    public Company(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public Company()
    {
    }

    public JobPosting PublishJob(
        string title,
        string description,
        string location,
        DateTime expirationDate,
        decimal salary,
        List<Criteria> criterias)
    {
        return new JobPosting(title, description, location, new Salary(salary),
            expirationDate, criterias, this);
    }
}