#nullable disable

using Common.Abstractions;
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Common;

namespace JobPostings.Domain.Aggregates.Companies;

public class Company : BaseEntity<CompanyId>, IAggregateRoot
{
    public string Name { get; }

    public Company(Guid companyId, string name)
    {
        Id = new CompanyId(companyId);
        Name = name;
    }

    public Company() { }

    public JobPosting PublishJob(
        string jobTitle,
        string jobDescription,
        string jobLocation,
        decimal jobSalary,
        DateTime jobExpirationDate,
        List<Criteria> jobCriterias)
    {
        return new JobPosting(jobTitle, jobDescription, jobLocation, jobSalary,
            jobExpirationDate, jobCriterias, this);
    }
}