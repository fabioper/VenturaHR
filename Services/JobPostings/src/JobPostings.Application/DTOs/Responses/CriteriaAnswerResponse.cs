namespace JobPostings.Application.DTOs.Responses;

public record CriteriaAnswerResponse
{
    public Guid Id { get; init; }
    public Guid CriteriaId { get; init; }
    public string CriteriaTitle { get; init; }
    public string CriteriaDescription { get; init; }
    public int Value { get; init; }
}