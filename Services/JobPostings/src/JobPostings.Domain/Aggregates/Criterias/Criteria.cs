#nullable disable

using Common.Abstractions;

namespace JobPostings.Domain.Aggregates.Criterias;

public class Criteria : BaseEntity, IAggregateRoot
{
    public string Title { get; }

    public string Description { get; }

    public int Weight { get; }

    public DesiredProfile DesiredProfile { get; }

    public Criteria(
        string title,
        string description,
        int weight,
        DesiredProfile desiredProfile)
    {
        Title = title;
        Description = description;
        Weight = weight;
        DesiredProfile = desiredProfile;
    }

    public Criteria() { } // Ef required
}