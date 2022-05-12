using Common;

namespace JobPostings.Domain.JobPostingAggregate;

public class Salary : ValueObject
{
    public decimal Value { get; private set; }

    public Salary(decimal value) => Value = value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}