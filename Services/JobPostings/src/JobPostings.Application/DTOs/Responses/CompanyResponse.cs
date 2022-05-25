namespace JobPostings.Application.DTOs.Responses;

public record CompanyResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}