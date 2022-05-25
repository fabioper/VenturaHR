using Common.Abstractions;

namespace JobPostings.Domain.Common;

public record ApplicantId(Guid Id) : EntityId
{
    public ApplicantId(string id) : this(Guid.Parse(id)) { }
}