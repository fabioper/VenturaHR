using Common.Abstractions;
using JobPostings.Domain.JobPostingAggregate;

namespace JobPostings.Domain.CompanyAggregate;

public class Company : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string ExternalId { get; private set; }

    private readonly List<JobPosting> _jobPostings;
    public IReadOnlyCollection<JobPosting> JobPostings => _jobPostings;

    public Company(string name) => Name = name;

    public Company() { }
}