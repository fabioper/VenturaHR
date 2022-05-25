namespace JobPostings.Domain.Aggregates.JobPostings;

public record ExpirationDate
{
    public DateTime Date { get; private set; }

    public ExpirationDate(DateTime date)
    {
        if (date <= DateTime.Now)
            throw new InvalidDataException("Data limite menor que data atual");
        Date = date;
    }

    public ExpirationDate() { }
}