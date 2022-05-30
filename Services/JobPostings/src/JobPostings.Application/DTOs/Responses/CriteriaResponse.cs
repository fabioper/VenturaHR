using JobPostings.Domain.Aggregates.Criterias;

#nullable disable

namespace JobPostings.Application.DTOs.Responses;

public record CriteriaResponse
{
    public string Title { get; init; }
    public Weight Weight { get; init; }
    public MinimumDesiredProfile MinimumDesiredProfile { get; init; }
}