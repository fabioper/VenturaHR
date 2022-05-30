using Common.Abstractions;
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Common;

#nullable disable

namespace JobPostings.Domain.Aggregates.Application;

public class CriteriaFullfillment : BaseEntity<CriteriaFullfillmentId>
{
    public Criteria Criteria { get; }
    public CriteriaFullfillmentValue Value { get; }

    public CriteriaFullfillment(Criteria criteria, CriteriaFullfillmentValue value)
    {
        Criteria = criteria;
        Value = value;
    }

    public CriteriaFullfillment() { } // Ef required
}