#nullable disable
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Application.DTOs.Requests;

public record CriteriaRequest
{
    public string Title { get; set; }
    public Weight Weight { get; set; }
    public MinimumDesiredProfile MinimumDesiredProfile { get; set; }
}