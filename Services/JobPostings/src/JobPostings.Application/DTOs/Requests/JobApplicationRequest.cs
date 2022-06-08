namespace JobPostings.Application.DTOs.Requests;

public record JobApplicationRequest
{
    public Guid JobPostingId { get; init; }
    public List<CriteriaAnswerRequest> CriteriaAnswers { get; init; }
}