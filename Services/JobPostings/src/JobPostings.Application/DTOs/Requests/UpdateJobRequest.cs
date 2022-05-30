namespace JobPostings.Application.DTOs.Requests;

#nullable disable

public record UpdateJobRequest
{
    public Guid Id { get; set; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Location { get; init; }
    public decimal Salary { get; init; }
    public DateTime ExpirationDate { get; init; }
    public List<CriteriaRequest> Criterias { get; init; }
};