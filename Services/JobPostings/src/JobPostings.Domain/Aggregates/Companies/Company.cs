#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Aggregates.Companies;

public class Company : BaseEntity<CompanyId>, IAggregateRoot
{
    public string Name { get; private set; }

    public Company(Guid companyId, string name)
    {
        Id = new CompanyId(companyId);
        Name = name;
    }

    public Company() { }

    public JobPosting PublishJob(
        string title,
        string description,
        string location,
        decimal salary,
        DateTime expiration,
        List<Criteria> criterias)
    {
        return new JobPosting(title, description, location, salary,
            expiration, criterias, this);
    }
}