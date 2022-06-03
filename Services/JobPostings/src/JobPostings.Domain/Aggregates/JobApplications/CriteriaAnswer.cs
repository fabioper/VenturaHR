#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Criterias;

namespace JobPostings.Domain.Aggregates.JobApplications;

public class CriteriaAnswer : BaseEntity
{
    public int Value { get; set; }
    
    public Guid CriteriaId { get; set; }

    public Criteria Criteria { get; }

    public CriteriaAnswer(Criteria criteria, int value)
    {
        Criteria = criteria;
        Value = value;
    }

    public CriteriaAnswer() { } // Ef required
}