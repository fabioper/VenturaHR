namespace JobPostings.Application.DTOs.Requests;

#nullable disable

public record RenewJobPostingRequest
{
    public DateTime NewExpiration { get; init; }
}