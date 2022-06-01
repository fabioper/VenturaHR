namespace JobPostings.Application.DTOs.Responses;

public record JobApplicationResponse
{
    public Guid Id { get; init; }
    public DateTime AppliedAt { get; init; }
    public JobPostingResponse JobPosting { get; init; }
    public List<CriteriaAnswerResponse> CriteriasAnswers { get; init; }
}