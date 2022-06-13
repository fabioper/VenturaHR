namespace JobPostings.Domain.Tests.Fixtures;

public class CriteriasDataFixture
{
    public static IEnumerable<(List<(int Profile, int Weight)>, double ExpectedResult)> TestCriterias()
    {
        yield return (new()
        {
            (Profile: 4, Weight: 5),
            (Profile: 4, Weight: 3),
            (Profile: 1, Weight: 1),
            (Profile: 4, Weight: 2),
        }, ExpectedResult: 3.72);

        yield return (new()
        {
            (Profile: 5, Weight: 5),
            (Profile: 1, Weight: 2),
            (Profile: 3, Weight: 3),
            (Profile: 5, Weight: 3),
        }, ExpectedResult: 3.92);
    }
}