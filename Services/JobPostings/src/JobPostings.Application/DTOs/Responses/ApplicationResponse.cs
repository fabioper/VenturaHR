namespace JobPostings.Application.DTOs.Responses;

#nullable disable

public record ApplicationResponse
{
    public ApplicantResponse Applicant { get; init; }
    public DateTime AppliedAt { get; init; }
}