using MediatR;

namespace JobPostings.Application.Commands.PostJob;

#nullable disable

public record PostJobCommand : IRequest
{
    public string Role { get; init; }
    public string Description { get; init; }
    public string Location { get; init; }
    public decimal Salary { get; init; }
    public DateTime ExpirationDate { get; init; }
    public string CompanyId { get; init; }
}