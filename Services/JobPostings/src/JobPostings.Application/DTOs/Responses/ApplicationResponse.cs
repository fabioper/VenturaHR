namespace JobPostings.Application.DTOs.Responses;

public record ApplicationResponse
{
    public ApplicantResponse Applicant { get; init; }
    public DateTime AppliedAt { get; init; }
}