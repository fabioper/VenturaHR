#nullable disable

using Common.Abstractions;

namespace JobPostings.Domain.CompanyAggregate;

public class Company : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string ExternalId { get; private set; }

    public Company(string name, string externalId)
    {
        Name = name;
        ExternalId = externalId;
    }

    public Company() { }
}