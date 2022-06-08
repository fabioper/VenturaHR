#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Criterias;

namespace JobPostings.Domain.Aggregates.JobApplications;

public class CriteriaAnswer : BaseEntity
{
    public int Value { get; }
    public Criteria Criteria { get; private set; }

    public CriteriaAnswer(Criteria criteria, int value)
    {
        Criteria = criteria;
        Value = value;
    }

    public CriteriaAnswer() { } // Ef required
}