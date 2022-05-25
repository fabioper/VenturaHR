namespace JobPostings.Application.DTOs.Requests;

#nullable disable

public record PostJobRequest
{
    public string Title { get; init; }
    public string Description { get; init; }
    public string Location { get; init; }
    public decimal Salary { get; init; }
    public DateTime ExpirationDate { get; init; }
    public string CompanyId { get; init; }
}