#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Aggregates.Companies;

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