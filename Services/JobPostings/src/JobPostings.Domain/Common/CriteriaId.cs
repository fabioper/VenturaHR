using Common.Abstractions;

namespace JobPostings.Domain.Common;

public record CriteriaId(Guid Id) : EntityId
{
    public CriteriaId() : this(Guid.NewGuid())
    {
    }
}