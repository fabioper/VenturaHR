#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Aggregates.JobApplications;

public class CriteriaAnswer : BaseEntity<CriteriaFullfillmentId>
{
    public Criteria Criteria { get; }
    public int Value { get; }

    public CriteriaAnswer(Criteria criteria, int value)
    {
        Criteria = criteria;
        Value = value;
    }

    public CriteriaAnswer() { } // Ef required
}