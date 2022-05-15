using System.Security.Claims;
using JobPostings.Api.Constants;
using JobPostings.Api.DTOs.Requests;
using JobPostings.Api.Extensions;
using JobPostings.Application.Models.Inputs;
using JobPostings.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPostings.Api.Controllers;

[ApiController]
[Route("job-postings")]
public class JobPostingsController : ControllerBase
{
    private readonly IJobPostingsService _jobPostingsService;

    public JobPostingsController(IJobPostingsService jobPostingsService) =>
        _jobPostingsService = jobPostingsService;

    [HttpPost]
    [Authorize(Policy = Policy.CompanyOnly)]
    public async Task<IActionResult> PostJob([FromBody] PostJobRequest request)
    {
        await _jobPostingsService.PostJob(
            new()
            {
                Role = request.Role,
                Description = request.Description,
                Location = request.Location,
                Salary = request.Salary,
                ExpirationDate = request.ExpirationDate,
                CompanyId = User.GetId(),
            }
        );

        return Ok();
    }
}