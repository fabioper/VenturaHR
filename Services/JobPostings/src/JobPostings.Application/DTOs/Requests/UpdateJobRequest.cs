namespace JobPostings.Application.DTOs.Requests;

public record UpdateJobRequest
{
    public Guid Id { get; set; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Location { get; init; }
    public decimal Salary { get; init; }
    public DateTime ExpirationDate { get; init; }
    public List<PostJobCriteriaRequest> Criterias { get; init; }
};