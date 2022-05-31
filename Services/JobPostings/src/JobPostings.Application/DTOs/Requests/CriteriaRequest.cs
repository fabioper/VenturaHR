#nullable disable
using JobPostings.Domain.Aggregates.Criterias;

namespace JobPostings.Application.DTOs.Requests;

public record CriteriaRequest
{
    public string Title { get; set; }
    public int Weight { get; set; }
    public DesiredProfile DesiredProfile { get; set; }
}