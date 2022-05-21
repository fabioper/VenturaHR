#nullable disable

using Common.Abstractions;

namespace JobPostings.Domain.CompanyAggregate;

public class Company : BaseEntity<CompanyId>, IAggregateRoot
{
    public string Name { get; private set; }

    public Company(string name, string externalId)
    {
        Id = new CompanyId(externalId);
        Name = name;
    }

    public Company() { }
}