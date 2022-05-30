using Common.Abstractions;
using JobPostings.Domain.Common;

#nullable disable

namespace JobPostings.Domain.Aggregates.Applicants;

public class Applicant : BaseEntity<ApplicantId>, IAggregateRoot
{
    public string Name { get; }

    public Applicant(string id, string name)
    {
        Id = new ApplicantId(id);
        Name = name;
    }

    public Applicant()
    {
    } // Ef required
}