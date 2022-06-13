using Common.Abstractions;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Domain.Specifications;

public class ExpiredMoreThanDaysSpecification : BaseSpecification<JobPosting>
{
    public ExpiredMoreThanDaysSpecification(int days)
    {
        Criteria = job =>
            job.Status == JobPostingStatus.Expired && (job.ExpireAt - DateTime.UtcNow) > TimeSpan.FromDays(days);

        Includes.Add(x => x.Company);
    }
}