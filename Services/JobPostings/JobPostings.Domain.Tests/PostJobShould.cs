using System;
using System.IO;
using JobPostings.Domain.CompanyAggregate;
using JobPostings.Domain.JobPostingAggregate;
using NUnit.Framework;

namespace JobPostings.Domain.Tests;

public class PostJobShould
{
    private Company _company;

    [SetUp]
    public void SetUp()
    {
        _company = new Company("Under Test");
    }

    [Test]
    public void IncreaseJobPostings_WithValidInput()
    {
        var jobPosting = new JobPosting(
            "Desenvolvedor Front-end",
            "Longa descrição",
            "Brasil",
            new Compensation(14000),
            new ExpirationDate(DateTime.Now.AddMonths(1))
        );

        _company.AddJobPosting(jobPosting);

        Assert.That(_company.JobPostings, Has.Exactly(1).Items);
    }

    [Test]
    public void ThrowExceptionIfExpirationDateLessThanCurrentDate()
    {
        Assert.Throws<InvalidDataException>(() =>
        {
            var jobPosting = new JobPosting(
                "Desenvolvedor Front-end",
                "Longa descrição",
                "Brasil",
                new Compensation(14000),
                new ExpirationDate(DateTime.Now.AddMonths(1))
            );

            _company.AddJobPosting(jobPosting);
        });
    }
}