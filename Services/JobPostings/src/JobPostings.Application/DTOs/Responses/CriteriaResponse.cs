using JobPostings.Domain.Aggregates.Criterias;

#nullable disable

namespace JobPostings.Application.DTOs.Responses;

public record CriteriaResponse
{
    public string Title { get; init; }
    public int Weight { get; init; }
    public DesiredProfile DesiredProfile { get; init; }
}