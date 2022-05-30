#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Aggregates.Criterias;

public class Criteria : BaseEntity<CriteriaId>, IAggregateRoot
{
    public string Title { get; }
    public Weight Weight { get; }
    public MininumDesiredProfile MininumDesiredProfile { get; }

    public Criteria(string title, Weight weight, MininumDesiredProfile mininumDesiredProfile)
    {
        Title = title;
        Weight = weight;
        MininumDesiredProfile = mininumDesiredProfile;
    }

    public Criteria() { } // Ef required
}