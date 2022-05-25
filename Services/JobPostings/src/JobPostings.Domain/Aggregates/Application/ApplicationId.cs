using Common.Abstractions;

namespace JobPostings.Domain.Aggregates.Application;

public record ApplicationId(Guid Id) : EntityId;