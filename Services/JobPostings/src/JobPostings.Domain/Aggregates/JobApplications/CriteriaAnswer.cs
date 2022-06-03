#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Criterias;

namespace JobPostings.Domain.Aggregates.JobApplications;

public class CriteriaAnswer : BaseEntity
{
    public int Value { get; }

    private Guid _criteriaId;
    public Criteria Criteria { get; private set; }

    public CriteriaAnswer(Guid criteriaId, int value)
    {
        _criteriaId = criteriaId;
        Value = value;
    }

    public CriteriaAnswer() { } // Ef required
}