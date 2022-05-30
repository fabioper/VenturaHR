namespace JobPostings.Application.DTOs.Requests;

public record ApplyToJobRequest
{
    public Guid ApplicantId { get; init; }
    public Guid JobPostingId { get; init; }
    public List<CriteriaFullfillmentRequest> CriteriaFullfillments { get; init; }
}