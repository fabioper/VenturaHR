namespace JobPostings.Application.DTOs.Responses;

public record JobApplicationResponse
{
    public Guid Id { get; init; }
    public double Average { get; init; }
    public DateTime AppliedAt { get; init; }
    public JobPostingResponse JobPosting { get; init; }
    public ApplicantResponse Applicant { get; init; }
    public List<CriteriaAnswerResponse> Answers { get; init; }
}