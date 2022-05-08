using Common;

namespace JobPostings.Domain.Aggregates.JobPostingAggregate;

public class ExpirationDate : ValueObject
{
    public DateTime Date { get; set; }

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
}