using Common;

namespace JobPostings.Domain.JobPostingAggregate;

public class Compensation : ValueObject
{
    public decimal Value { get; private set; }

    public Compensation(decimal value) => Value = value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}