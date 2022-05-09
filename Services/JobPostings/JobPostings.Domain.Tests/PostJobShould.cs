using System;
using System.IO;
using JobPostings.Domain.CompanyAggregate;
using NUnit.Framework;

namespace JobPostings.Domain.Tests;

#nullable disable

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
        _company.AddJobPosting("Desenvolvedor Front-end",
            "Longa descrição",
            "Brasil",
            14000,
            DateTime.Now.AddMonths(1));

        Assert.That(_company.JobPostings, Has.Exactly(1).Items);
    }

    [Test]
    public void ThrowExceptionIfExpirationDateLessThanCurrentDate()
    {
        Assert.Throws<InvalidDataException>(() =>
        {
            _company.AddJobPosting("Desenvolvedor Front-end",
                "Longa descrição",
                "Brasil",
                14000,
                DateTime.Now.AddMonths(-1));
        });
    }
}