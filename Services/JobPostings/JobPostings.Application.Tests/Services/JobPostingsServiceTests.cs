using Common;
using JobPostings.Application.Services.Concretes;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.JobPostingAggregate;
using Moq;
using NUnit.Framework;

#nullable disable

namespace JobPostings.Application.Tests.Services;

public class JobPostingsServiceTests
{
    private IJobPostingsService _jobPostingsService;
    private Mock<IRepository<JobPosting>> _jobPostingsRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _jobPostingsRepositoryMock = new Mock<IRepository<JobPosting>>();
        _jobPostingsService = new JobPostingsService(_jobPostingsRepositoryMock.Object);
    }

    [Test]
    public void PublishJobPosting_WithValidInput_ShouldSuccess()
    {
        Assert.Pass();
    }
}