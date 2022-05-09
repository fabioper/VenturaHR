using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace JobPostings.Api.Tests.Controllers;

#nullable disable

public class JobPostingsControllerTests
{
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        var factory = new ApplicationFactory<Program>();
        _client = factory.CreateClient();
    }

    [Test]
    public async Task ShouldSuccess()
    {
        var response = await _client.PostAsync("/job-postings", null);
        response.EnsureSuccessStatusCode();
    }
}