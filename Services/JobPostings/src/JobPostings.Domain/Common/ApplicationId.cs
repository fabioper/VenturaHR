using Common.Abstractions;

namespace JobPostings.Domain.Common;

public record ApplicationId(Guid Id) : EntityId;