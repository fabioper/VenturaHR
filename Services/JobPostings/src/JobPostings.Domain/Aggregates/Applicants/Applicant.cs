using Common.Abstractions;

namespace JobPostings.Domain.Aggregates.Applicants;

public class Applicant : BaseEntity<ApplicantId>, IAggregateRoot
{
    public string Name { get; }

    public Applicant(string name) => Name = name;

    public Applicant() { } // Ef required
}