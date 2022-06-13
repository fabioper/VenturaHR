using Common.Abstractions;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Specifications;

public class ExpiringJobPostingsSpecification : BaseSpecification<JobPosting>
{
    public ExpiringJobPostingsSpecification()
    {
        var currentDate = DateTime.UtcNow;
        Criteria = x => x.ExpireAt - currentDate <= TimeSpan.FromDays(1) && x.ExpireAt - currentDate > TimeSpan.Zero;
        Includes.Add(x => x.Company);
    }
}