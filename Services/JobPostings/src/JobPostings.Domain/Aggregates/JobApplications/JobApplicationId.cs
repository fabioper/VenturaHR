using Common.Abstractions;

namespace JobPostings.Domain.Aggregates.JobApplications;

public record JobApplicationId(Guid Id) : EntityId;