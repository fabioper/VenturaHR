using Common.Abstractions;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Specifications;

public class ExpiredJobPostingsSpecification : BaseSpecification<JobPosting>
{
    public ExpiredJobPostingsSpecification()
    {
        Criteria = posting => posting.Status == JobPostingStatus.Published && posting.ExpireAt <= DateTime.UtcNow;
        Includes.Add(x => x.Company);
    }
}