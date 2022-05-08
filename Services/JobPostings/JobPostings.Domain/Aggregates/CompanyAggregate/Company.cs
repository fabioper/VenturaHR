using Common;
using JobPostings.Domain.Aggregates.JobPostingAggregate;

namespace JobPostings.Domain.Aggregates.CompanyAggregate;

public class Company : IAggregateRoot
{
    public string Name { get; private set; }
    private readonly List<JobPosting> _jobPostings;
    public IReadOnlyCollection<JobPosting> JobPostings => _jobPostings;

    public Company(string name)
    {
        Name = name;
        _jobPostings = new List<JobPosting>();
    }

    public Company() { }

    public void AddJobPosting(JobPosting jobPosting) => _jobPostings.Add(jobPosting);
}