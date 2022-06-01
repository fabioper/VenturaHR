#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Aggregates.JobApplications;

public class CriteriaAnswer : BaseEntity<CriteriaAnswerId>
{
    public int Value { get; set; }
    
    public CriteriaId CriteriaId { get; set; }

    public Criteria Criteria { get; }

    public CriteriaAnswer(Criteria criteria, int value)
    {
        Criteria = criteria;
        Value = value;
    }

    public CriteriaAnswer() { } // Ef required
}