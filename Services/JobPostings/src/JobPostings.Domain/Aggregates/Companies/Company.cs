#nullable disable

using Common.Abstractions;
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
}