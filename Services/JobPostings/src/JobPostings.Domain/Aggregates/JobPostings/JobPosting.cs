using Common.Abstractions;
using Common.Guards;
using JobPostings.CrossCutting.Exceptions;
using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Aggregates.Criterias;

namespace JobPostings.Domain.Aggregates.JobPostings;

#nullable disable

public class JobPosting : BaseEntity, IAggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Location { get; private set; }
    public Salary Salary { get; private set; }
    public DateTime ExpireAt { get; private set; }
    public JobPostingStatus Status { get; private set; }
    public List<Criteria> Criterias { get; private set; }
    public Company Company { get; private set; }
    public double Average { get; private set; }

    internal JobPosting(
        string title,
        string description,
        string location,
        Salary salary,
        DateTime expireAt,
        List<Criteria> criterias,
        Company company)
    {
        Guard.Against.NullOrEmpty(title, nameof(title));
        Guard.Against.NullOrEmpty(description, nameof(description));

        Title = title;
        Description = description;
        Location = location;
        Salary = salary;
        ExpireAt = expireAt;
        Criterias = criterias;
        Status = JobPostingStatus.Published;
        Company = company;
        Average = CalculateAverage();
    }
    
    public bool CanBeApplied => Status == JobPostingStatus.Published;

    public void UpdateStatus(JobPostingStatus newStatus) => Status = newStatus;

    public void UpdateTitle(string newTitle)
    {
        Guard.Against.NullOrEmpty(newTitle, nameof(newTitle));
        Title = newTitle;
    }

    public void UpdateDescription(string description)
    {
        Guard.Against.NullOrEmpty(description, nameof(description));
        Description = description;
    }

    public void UpdateLocation(string newLocation)
    {
        Guard.Against.NullOrEmpty(newLocation, nameof(newLocation));
        Location = newLocation;
    }

    public void UpdateSalary(decimal newSalary) => Salary = new Salary(newSalary);

    public void UpdateCriterias(List<Criteria> criterias) => Criterias = criterias;

    private double CalculateAverage()
    {
        if (!Criterias.Any())
            return 0;

        return Criterias.Sum(x => (int)x.DesiredProfile * x.Weight) / (double)Criterias.Sum(x => x.Weight);
    }

    public JobPosting() { } // Ef required

    public void Renew(DateTime newExpiration)
    {
        if (!CanBeRenewed)
            throw new UnableToRenewException();

        ExpireAt = newExpiration;
        Status = JobPostingStatus.Published;
    }

    private bool CanBeRenewed => Status != JobPostingStatus.Closed;
}