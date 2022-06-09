namespace JobPostings.Application.DTOs.Responses;

public record ApplicantResponse
{
    public string Email { get; init; }
    public string Name { get; init; }
}