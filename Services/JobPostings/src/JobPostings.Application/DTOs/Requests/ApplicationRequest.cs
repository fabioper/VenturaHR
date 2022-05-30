namespace JobPostings.Application.DTOs.Requests;

#nullable disable

public record ApplicationRequest
{
    public Guid ApplicantId { get; init; }
    public Guid JobPostingId { get; init; }
    public List<CriteriaFullfillmentRequest> CriteriaFullfillments { get; init; }
}