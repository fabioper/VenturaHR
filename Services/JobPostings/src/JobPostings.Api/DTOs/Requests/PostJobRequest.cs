namespace JobPostings.Api.DTOs.Requests;

public record PostJobRequest
{
    public string Role { get; init; }
    public string Description { get; init; }
    public string Location { get; init; }
    public decimal Salary { get; init; }
    public DateTime ExpirationDate { get; init; }
}