using JobPostings.CrossCutting.Exceptions;
using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Tests.Fixtures;

#nullable disable

namespace JobPostings.Domain.Tests.Entities;

public class JobPostingShould
{
    private Company _company;

    [SetUp]
    public void SetUp()
        => _company = new Company(Guid.NewGuid(), "Empresa", "empresa@empresa.com", "99999999999");

    [Test]
    public void CreateJobPostingWithPublishedStatus()
    {
        var jobPosting = _company.PublishJob(
            "Desenvolvedor",
            "Descrição",
            "Rio de Janeiro, RJ",
            DateTime.Now.AddMonths(1), 2500,
            new List<Criteria>()
        );

        Assert.That(jobPosting.Status, Is.EqualTo(JobPostingStatus.Published));
    }

    [Test]
    public void ThrowWhenTryingToRenewClosedJobPosting()
    {
        var jobPosting = _company.PublishJob("Desenvolvedor",
            "Descrição",
            "Rio de Janeiro, RJ",
            DateTime.Now.AddMonths(1), 2500,
            new List<Criteria>());

        jobPosting.UpdateStatus(JobPostingStatus.Closed);

        Assert.Throws<UnableToRenewException>(() => jobPosting.Renew(jobPosting.ExpireAt.AddMonths(1)));
    }

    [TestCaseSource(typeof(CriteriasDataFixture), nameof(CriteriasDataFixture.TestCriterias))]
    public void CalculateAverageBasedOnGivenCriterias((List<(int Profile, int Weight)> Values, double Result) sources)
    {
        var criterias = sources.Values.Select(
            criteria => new Criteria("Criteria Name", "Criteria Description", criteria.Profile,
                (DesiredProfile)criteria.Weight)).ToList();

        var jobPosting = _company.PublishJob(
            "Desenvolvedor",
            "Descrição",
            "Rio de Janeiro, RJ",
            DateTime.Now.AddMonths(1), 2500,
            criterias
        );

        Assert.That(jobPosting.Average, Is.EqualTo(sources.Result).Within(1.5));
    }
}