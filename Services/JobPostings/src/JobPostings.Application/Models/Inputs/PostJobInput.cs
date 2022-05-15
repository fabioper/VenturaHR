namespace JobPostings.Application.Models.Inputs;

#nullable disable

public record PostJobInput
{
    public string Role { get; init; }
    public string Description { get; init; }
    public string Location { get; init; }
    public decimal Salary { get; init; }
    public DateTime ExpirationDate { get; init; }
    public string CompanyId { get; init; }
}