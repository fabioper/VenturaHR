namespace JobPostings.Application.DTOs.Responses;

public record CriteriaAnswerResponse
{
    public CriteriaResponse Criteria { get; init; }
    public int Value { get; init; }
}