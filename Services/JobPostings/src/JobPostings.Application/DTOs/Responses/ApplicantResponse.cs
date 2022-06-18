namespace JobPostings.Application.DTOs.Responses;

public record ApplicantResponse
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
}