#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Aggregates.Criterias;

public class Criteria : BaseEntity<CriteriaId>, IAggregateRoot
{
    public string Title { get; }
    public Weight Weight { get; }
    public MinimumDesiredProfile MinimumDesiredProfile { get; }

    public Criteria(string title, Weight weight, MinimumDesiredProfile minimumDesiredProfile)
    {
        Title = title;
        Weight = weight;
        MinimumDesiredProfile = minimumDesiredProfile;
    }

    public Criteria() { } // Ef required
}