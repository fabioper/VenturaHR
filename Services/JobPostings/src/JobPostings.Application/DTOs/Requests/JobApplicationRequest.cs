namespace JobPostings.Application.DTOs.Requests;

#nullable disable

public record JobApplicationRequest
{
    public Guid JobPostingId { get; init; }
    public List<CriteriaAnswerRequest> CriteriaAnswers { get; init; }
}