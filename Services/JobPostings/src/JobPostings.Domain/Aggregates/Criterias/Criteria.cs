#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Aggregates.Criterias;

public class Criteria : BaseEntity<CriteriaId>, IAggregateRoot
{
    public string Title { get; }
    public int Weight { get; }
    public DesiredProfile DesiredProfile { get; }

    public Criteria(string title, int weight, DesiredProfile desiredProfile)
    {
        Title = title;
        Weight = weight;
        DesiredProfile = desiredProfile;
    }

    public Criteria() { } // Ef required
}