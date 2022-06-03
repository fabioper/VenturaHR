#nullable disable

using Common.Abstractions;

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
}