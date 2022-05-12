using Common;

namespace JobPostings.Domain.JobPostingAggregate;

public class ExpirationDate : ValueObject
{
    public DateTime Date { get; private set; }

    public ExpirationDate(DateTime date)
    {
        if (date <= DateTime.Now)
            throw new InvalidDataException("Data limite menor que data atual");
        Date = date;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Date;
    }

    public bool HasPassed(DateTime date) => Date > date;
}