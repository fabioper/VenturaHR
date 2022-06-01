using Common.Abstractions;

namespace JobPostings.Domain.Common;

public record CriteriaAnswerId(Guid Id) : EntityId
{
    public CriteriaAnswerId() : this(Guid.NewGuid()) { }
}