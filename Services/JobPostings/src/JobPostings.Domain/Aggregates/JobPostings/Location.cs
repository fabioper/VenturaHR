#nullable disable

using Common.Guards;

namespace JobPostings.Domain.Aggregates.JobPostings;

public record Location
{
    public string Place { get; private set; }

    public Location(string location)
    {
        Guard.Against.NullOrEmpty(location, nameof(location));
        Place = location;
    }

    public Location() { }
}