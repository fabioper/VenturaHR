namespace JobPostings.Application.DTOs.Responses;

#nullable disable

public record JobPostingResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Location { get; init; }
    public decimal Salary { get; init; }
    public List<CriteriaResponse> Criterias { get; init; }
    public DateTime ExpirationDate { get; init; }
    public CompanyResponse Company { get; init; }
}