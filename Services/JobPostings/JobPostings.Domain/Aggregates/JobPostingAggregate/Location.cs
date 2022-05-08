using Common;
using Common.Guards;

namespace JobPostings.Domain.Aggregates.JobPostingAggregate;

public class Location : ValueObject
{
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }

    public Location(string city, string state, string country)
    {
        Guard.Against.NullOrEmpty(city, nameof(city));
        Guard.Against.NullOrEmpty(state, nameof(state));
        Guard.Against.NullOrEmpty(country, nameof(country));

        City = city;
        State = state;
        Country = country;
    }

    public Location(string country)
    {
        Guard.Against.NullOrEmpty(country, nameof(country));
        Country = country;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return State;
        yield return Country;
    }
}