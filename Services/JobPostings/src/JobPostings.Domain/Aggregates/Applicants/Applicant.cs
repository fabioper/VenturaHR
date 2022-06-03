#nullable disable

using Common.Abstractions;

namespace JobPostings.Domain.Aggregates.Applicants;

public class Applicant : BaseEntity, IAggregateRoot
{
    public string Name { get; }

    public Applicant(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Applicant() { } // Ef required
}