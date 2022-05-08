using Common;

namespace JobPostings.Domain.Aggregates.JobPostingAggregate;

public class Compensation : ValueObject
{
    public decimal Value { get; set; }

    public Compensation(decimal value) => Value = value;

    protected override IEnumerable<object> GetEqualityComponents() { yield return Value; }
}