using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Application.DTOs.Responses;

#nullable disable

public record JobPostingResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Location { get; init; }
    public decimal Salary { get; init; }
    public double Average { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public DateTime ExpireAt { get; init; }
    public CompanyResponse Company { get; init; }
    public List<CriteriaResponse> Criterias { get; init; }
    public JobPostingStatus Status { get; set; }
}