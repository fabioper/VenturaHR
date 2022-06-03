#nullable disable

using Common.Abstractions;

namespace JobPostings.Domain.Aggregates.Criterias;

public class Criteria : BaseEntity, IAggregateRoot
{
    public string Title { get; }
    public int Weight { get; }
    public DesiredProfile DesiredProfile { get; }

    public Criteria(string title, int weight, DesiredProfile desiredProfile)
    {
        Id = Guid.NewGuid();
        Title = title;
        Weight = weight;
        DesiredProfile = desiredProfile;
    }

    public Criteria() { } // Ef required
}