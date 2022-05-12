using Common;
using Common.Guards;

#nullable disable

namespace JobPostings.Domain.JobPostingAggregate;

public class Location : ValueObject
{
    public string Place { get; private set; }

    public Location(string location)
    {
        Guard.Against.NullOrEmpty(location, nameof(location));
        Place = location;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Place;
    }

    public Location() { } // Ef required
}