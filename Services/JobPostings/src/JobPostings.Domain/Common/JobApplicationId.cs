using Common.Abstractions;

namespace JobPostings.Domain.Common;

public record JobApplicationId(Guid Id) : EntityId
{
    public JobApplicationId() : this(Guid.NewGuid()) { }
}