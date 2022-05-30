using JobPostings.Domain.Aggregates.Application;

namespace JobPostings.Application.DTOs.Requests;

public record CriteriaFullfillmentRequest
{
    public Guid CriteriaId { get; init; }
    public CriteriaFullfillmentValue Value { get; init; }
}