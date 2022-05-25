using Common.Abstractions;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Aggregates.JobPostings;

public class Criteria : BaseEntity<CriteriaId>
{
    public string Title { get; }

    public Criteria(string title) => Title = title;
}