namespace JobPostings.Application.DTOs.Requests;

public record CriteriaAnswerRequest
{
    public Guid CriteriaId { get; init; }
    public int Value { get; init; }
}