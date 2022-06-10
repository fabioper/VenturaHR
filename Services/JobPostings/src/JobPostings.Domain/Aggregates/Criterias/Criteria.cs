#nullable disable

using Common.Abstractions;

namespace JobPostings.Domain.Aggregates.Criterias;

public class Criteria : BaseEntity, IAggregateRoot
{
    public string Title { get; private set; }

    public string Description { get; private set; }

    public int Weight { get; private set; }

    public DesiredProfile DesiredProfile { get; private set; }

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