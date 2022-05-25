using Common.Abstractions;

namespace JobPostings.Domain.Aggregates.Applicants;

public record ApplicantId(Guid Id) : EntityId
{
    public ApplicantId(string id) : this(Guid.Parse(id)) { }
}