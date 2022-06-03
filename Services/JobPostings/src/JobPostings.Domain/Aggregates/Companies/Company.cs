#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Aggregates.Companies;

public class Company : BaseEntity, IAggregateRoot
{
    public string Name { get; }

    public Company(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Company() { } // ef required

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