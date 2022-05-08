using Common;
using JobPostings.Domain.JobPostingAggregate;

namespace JobPostings.Domain.CompanyAggregate;

public class Company : Entity, IAggregateRoot
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

    public void AddJobPosting(JobPosting jobPosting)
    {
        _jobPostings.Add(jobPosting);
    }
}